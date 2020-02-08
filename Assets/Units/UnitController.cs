using System;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private UnitDefinition unitDefinition;
        [SerializeField] private GameObject target;

        private SphereCollider _attackRangeCollider;
        private SphereCollider _viewRangeCollider;

        private NavMeshAgent _navMeshAgent;

        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.destination = target.transform.position;
            _navMeshAgent.speed = unitDefinition.MovementSpeed;

            var parentTransform = transform.parent.gameObject.transform;
            _attackRangeCollider = parentTransform.Find("AttackRange").GetComponent<SphereCollider>();
            _attackRangeCollider.radius = unitDefinition.AttackRange;

        }

        void Attack(GameObject[] targets)
        {
            foreach (GameObject target in targets)
            {
                Debug.Log("start attacking " + target.name);    
            }

            
        }

        void Move(Vector3 position)
        {
            Debug.Log("Move to position " + position);
            _navMeshAgent.destination = position;
        }

    }
}