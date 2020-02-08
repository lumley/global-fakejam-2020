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
            
            _attackRangeCollider = gameObject.AddComponent<SphereCollider>();
            _attackRangeCollider.radius = unitDefinition.AttackRange;

            _viewRangeCollider = gameObject.AddComponent<SphereCollider>();
            _viewRangeCollider.radius = unitDefinition.ViewRange;
            
            
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}