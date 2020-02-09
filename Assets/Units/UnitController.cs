using System;
using System.Collections;
using System.Collections.Generic;
using Fakejam.GameUtilities;
using Fakejam.Input;
using Fakejam.Players;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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

        private readonly Collider[] _potentialTargets = new Collider[20];
        private readonly List<UnitController> _targetUnitControllers = new List<UnitController>();
        private PoolingManager _poolingManager;

        public PlayerType PlayerType => playerType;

        public UnitDefinition UnitDefinition => unitDefinition;

        public int Health => _health;

        public SpriteRenderer displaySprite;

        public UnitEvent OnUnitDied;
        private Transform _transformWhereToSpawnShots;

        private void Awake()
        {
            playerType = PlayerType.Player;
        }

        private void Start()
        {
            _poolingManager = Toolbox.Get<PoolingManager>();
            _healthBar = GetComponentInChildren<HealthBar>();
            _healthBar.SetHealth(1);
            _transformWhereToSpawnShots = Toolbox.Get<InputManager>().CombatSceneManager.transform;
            
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = unitDefinition.MovementSpeed;
        
            _health = unitDefinition.MaxHealth;
            _attackCoroutine = StartCoroutine(AttackRepeat());
            _alreadyStarted = true;
        }

        public void setOwner(PlayerType type)
        {
            playerType = type;
            displaySprite.color = playerType == PlayerType.Player ?
                new Color(1, 1, 1) :
                new Color(1, 0.5f, 0.5f);
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
            _health -= damage;

            _healthBar.SetHealth(_health/(float)unitDefinition.MaxHealth);
            if (_health <= 0)
            {
                transform.parent.gameObject.SetActive(false);
                OnUnitDied?.Invoke(this);
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
            var shot = _poolingManager.Create(UnitDefinition.ShotPrefab, _transformWhereToSpawnShots);
            shot.SetTarget(transform, enemy, unitDefinition.Damage, UnitDefinition.ShotPrefab);
        }
        
        [Serializable]
        public class UnitEvent : UnityEvent<UnitController>
        {
            
        }
    }
}