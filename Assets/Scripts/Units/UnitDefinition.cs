using Fakejam.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fakejam.Units
{
    [CreateAssetMenu(fileName = nameof(UnitDefinition), order = 0, menuName = AssetMenuConstants.ScriptableObjectsMenu + nameof(UnitDefinition))]
    public class UnitDefinition : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _damage;
        [SerializeField] private int _defense;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRange;
        
        [SerializeField, Tooltip("Units/second")] private float _movementSpeed;

        [Header("Squad Vars")]

        [SerializeField] private int _squadSize;
        [SerializeField] private float _influenceRange;

        [Header("To Spawn")] [SerializeField] private SquadMember _prefabOfUnit;

        [Header("Unit Icon")] [SerializeField] private Sprite _icon;

        [Header("While In Combat")] [SerializeField] private SquadBehavior _squadBehavior;

        [Header("Production")] [SerializeField]
        private float _productionTime;

        public int MaxHealth => _maxHealth;

        public int Damage => _damage;

        public int Defense => _defense;

        public float AttackSpeed => _attackSpeed;

        public float AttackRange => _attackRange;

        public float MovementSpeed => _movementSpeed;

        public float SquadSize => _squadSize;
        public float InfluenceRange => _influenceRange;

        public SquadMember PrefabOfUnit => _prefabOfUnit;

        public Sprite Icon => _icon;

        public SquadBehavior SquadBehavior => _squadBehavior;

        public float ProductionTime => _productionTime;
    }
}