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
        [SerializeField] private float _viewRange;
        [SerializeField, Tooltip("Units/second")] private float _movementSpeed;

        [Header("To Spawn")] [SerializeField] private GameObject _prefabOfUnit;

        [Header("Unit Icon")] [SerializeField] private Sprite _icon;

        public int MaxHealth => _maxHealth;

        public int Damage => _damage;

        public int Defense => _defense;

        public float AttackSpeed => _attackSpeed;

        public float AttackRange => _attackRange;
        public float ViewRange => _viewRange;

        public float MovementSpeed => _movementSpeed;

        public GameObject PrefabOfUnit => _prefabOfUnit;

        public Sprite Icon => _icon;
    }
}