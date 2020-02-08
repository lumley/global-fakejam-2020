using System;
using System.Collections.Generic;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private UnitDefinition unitDefinition;

        private SphereCollider _attackRangeCollider;

        private NavMeshAgent _navMeshAgent;

        private int _health;

        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = unitDefinition.MovementSpeed;

            var parentTransform = transform.parent.gameObject.transform;
            _attackRangeCollider = parentTransform.Find("AttackRange").GetComponent<SphereCollider>();
            _attackRangeCollider.radius = unitDefinition.AttackRange;

            _health = unitDefinition.MaxHealth;
        }

        void Attack(IEnumerable<GameObject> targets)
        {
            if (gameObject.activeSelf == false)
            {
                Debug.Log("Unit cannot attack as it is already dead");
                return;
            }

            foreach (GameObject target in targets)
            {
                var unitController = target.GetComponent<UnitController>();
                if (unitController == null)
                {
                    Debug.Log("tried to attack non attackable object '" + target.name + "'");
                    continue;
                }

                unitController.TakeDamage(unitDefinition.Damage);
                Debug.Log("attacking " + target.name);
            }
        }

        void TakeDamage(int damage)
        {
            if (gameObject.activeSelf == false)
            {
                Debug.Log("Unit cannot take damage as it is already dead");
                return;
            }

            _health -= damage;
            var objectName = gameObject.name;
            Debug.Log("Unit '" + objectName + " took " + damage + "damage");
            if (_health <= 0)
            {
                Debug.Log("Unit '" + objectName + "' died");
                gameObject.SetActive(false);
            }
        }

        public void Move(Vector3 position)
        {
            if (gameObject.activeSelf == false)
            {
                Debug.Log("Unit cannot move as it is already dead");
                return;
            }

            Debug.Log("Move to position " + position);
            _navMeshAgent.destination = position;
        }
    }
}