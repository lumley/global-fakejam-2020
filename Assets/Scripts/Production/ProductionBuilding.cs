using Fakejam.Units;
using UnityEngine;

namespace Fakejam.Production
{
    public class ProductionBuilding : MonoBehaviour
    {
        [SerializeField] private UnitDefinition _producingUnit;
        [SerializeField] private int _currentUnitCount;
        
        [Header("References")]
        [SerializeField] private CombatClickable _clickable;

        public void SetProduction(UnitDefinition producingUnit, int initialCount)
        {
            _producingUnit = producingUnit;
            _currentUnitCount = initialCount;
        }

        public (UnitDefinition, int) GetTotalProduction()
        {
            return (_producingUnit, _currentUnitCount);
        }

        private void OnEnable()
        {
            _clickable.OnClick.AddListener(OnProductionClicked);
        }

        private void OnDisable()
        {
            _clickable.OnClick.RemoveListener(OnProductionClicked);
        }

        private void OnProductionClicked()
        {
            // Deal with the production here
        }
    }
}