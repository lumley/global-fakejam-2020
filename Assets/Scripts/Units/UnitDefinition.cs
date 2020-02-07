using UnityEngine;

namespace Fakejam.Units
{
    [CreateAssetMenu(fileName = nameof(UnitDefinition), order = 0, menuName = AssetMenuConstants.ScriptableObjectsMenu + nameof(UnitDefinition))]
    public class UnitDefinition : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _damage;
        [SerializeField] private int _defense;
        [SerializeField] private float _range;
        [SerializeField, Tooltip("Units/second")] private float _movementSpeed;

        public int MaxHealth => _maxHealth;

        public int Damage => _damage;

        public int Defense => _defense;

        public float Range => _range;

        public float MovementSpeed => _movementSpeed;
    }
}