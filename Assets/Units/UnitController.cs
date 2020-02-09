using System;
using System.Collections;
using System.Collections.Generic;
using Fakejam.Players;
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

        private NavMeshAgent _navMeshAgent;

        [SerializeField] private int _health;

        [SerializeField] private PlayerType playerType;

        [SerializeField] private LayerMask layerMask;
        private Coroutine _attackCoroutine;

        private HealthBar _healthBar;

        private bool _alreadyStarted = false;

        void Start()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
            _healthBar.SetHealth(1);

            playerType = PlayerType.Player;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = unitDefinition.MovementSpeed;

            _health = unitDefinition.MaxHealth;
            _attackCoroutine = StartCoroutine(AttackRepeat());
            _alreadyStarted = true;
        }

        public void setOwner(PlayerType type)
        {
            playerType = type;
        }

        private void OnDisable()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }

        private IEnumerator AttackRepeat()
        {
            while (true)
            {
                if (gameObject.activeSelf)
                {
                    var targets = Physics.OverlapSphere(transform.position, unitDefinition.AttackRange, layerMask);

                    foreach (Collider target in targets)
                    {
                        var unitController = target.gameObject.GetComponent<UnitController>();

                        if (unitController == this)
                        {
                            continue;
                        }

                        if (unitController == null)
                        {
                            Debug.Log("tried to attack non attackable object '" + target.name + "'");
                            continue;
                        }

                        if (unitController.playerType != playerType)
                        {
                            unitController.TakeDamage(unitDefinition.Damage);
                        }

                        Debug.Log("attacking " + target.name);
                    }
                }
                else
                {
                    Debug.Log("Unit cannot attack as it is already dead");
                }

                yield return new WaitForSeconds(unitDefinition.AttackSpeed);
            }
        }


        void TakeDamage(int damage)
        {
            if (_alreadyStarted == false)
            {
                return;
            }

            if (gameObject.activeSelf == false)
            {
                Debug.Log("Unit cannot take damage as it is already dead");
                return;
            }

            var objectName = transform.parent.name;
            Debug.Log(objectName + " health " + _health);
            _health -= damage;

            _healthBar.SetHealth(_health/(float)unitDefinition.MaxHealth);
            Debug.Log("Unit '" + objectName + " took " + damage + "damage");
            if (_health <= 0)
            {
                Debug.Log("Unit '" + objectName + "' died");
                transform.parent.gameObject.SetActive(false);
            }
        }

        public void Move(Vector3 position)
        {
            if (gameObject.activeSelf == false)
            {
                Debug.Log("Unit cannot move as it is already dead");
                return;
            }

            //Debug.Log("Move to position " + position);
            _navMeshAgent.destination = position;
        }
    }
}