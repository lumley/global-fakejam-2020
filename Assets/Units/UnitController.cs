using System.Collections;
using System.Collections.Generic;
using Fakejam.GameUtilities;
using Fakejam.Input;
using Fakejam.Players;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.AI;

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

        private bool _alreadyStarted = false;

        private readonly Collider[] _potentialTargets = new Collider[20];
        private readonly List<UnitController> _targetUnitControllers = new List<UnitController>();
        private PoolingManager _poolingManager;

        public PlayerType PlayerType => playerType;

        public UnitDefinition UnitDefinition => unitDefinition;

        public int Health => _health;

        // Start is called before the first frame update
        private void Awake()
        {
            playerType = PlayerType.Player;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = unitDefinition.MovementSpeed;
        }

        void Start()
        {
            _poolingManager = Toolbox.Get<PoolingManager>();
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
            while (this != null)
            {
                if (gameObject.activeSelf)
                {
                    int actualTargetCount = Physics.OverlapSphereNonAlloc(transform.position, unitDefinition.AttackRange,_potentialTargets, layerMask);
                    _targetUnitControllers.Clear();
                    for (int i = 0; i < actualTargetCount; i++)
                    {
                        var enemyTarget = _potentialTargets[i].GetComponent<UnitController>();
                        if (enemyTarget != null && enemyTarget.playerType != playerType)
                        {
                            _targetUnitControllers.Add(enemyTarget);
                        }
                    }
                    unitDefinition.AttackBehavior.Attack(this, _targetUnitControllers);
                }
                else
                {
                    Debug.Log("Unit cannot attack as it is already dead");
                }

                yield return new WaitForSeconds(unitDefinition.AttackSpeed);
            }
        }


        public void TakeDamage(int damage)
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

        public void Shoot(UnitController enemy)
        {
            var shot = _poolingManager.Create(UnitDefinition.ShotPrefab);
            shot.SetTarget(transform, enemy, unitDefinition.Damage, UnitDefinition.ShotPrefab);
        }
    }
}