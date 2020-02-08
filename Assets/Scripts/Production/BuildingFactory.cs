using System.Collections.Generic;
using Fakejam.Input;
using Fakejam.Players;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Fakejam.Production
{
    public class BuildingFactory : MonoBehaviour
    {
        [SerializeField] private ProductionBuilding _productionPrefab;
        [SerializeField] private Transform[] _transformsWhereToSpawn;
        [SerializeField] private Transform _transformWhereUnitsWillWalk;
        public UnityEvent OnProductionsWritten;

        private List<ProductionBuilding> _productionBuildings = new List<ProductionBuilding>();
        
        private void Start()
        {
            var battleManager = Toolbox.Get<BattleManager>();

            var squadsForOwner = battleManager.GetSquadsForOwner(PlayerType.Player);
            Dictionary<UnitDefinition, int> unitToCountMap = new Dictionary<UnitDefinition, int>(squadsForOwner.Count);
            foreach (var squad in squadsForOwner)
            {
                unitToCountMap.TryGetValue(squad.UnitDefinition, out int count);
                unitToCountMap[squad.UnitDefinition] = count + squad.Count;
            }
            
            var allUnitDefinitions = battleManager.AllUnitDefinitions;
            for (var i = 0; i < allUnitDefinitions.Length; i++)
            {
                var unitDefinition = allUnitDefinitions[i];
                unitToCountMap.TryGetValue(unitDefinition, out int unitCount);
                var transformWhereToSpawn = _transformsWhereToSpawn[i];
                var productionBuilding = Instantiate(_productionPrefab, transformWhereToSpawn);
                productionBuilding.SetProduction(unitDefinition, unitCount, _transformWhereUnitsWillWalk);
                _productionBuildings.Add(productionBuilding);
            }
        }

        public void WriteProductionsToBattleManager()
        {
            var progressionManager = Toolbox.Get<ProgressionManager>();
            var squads = new List<Squad>();
            
            foreach (var productionBuilding in _productionBuildings)
            {
                var (unitDefinition, unitCount) = productionBuilding.GetTotalProduction();
                squads.Add(new Squad
                {
                    Count = unitCount,
                    UnitDefinition = unitDefinition,
                    Owner = PlayerType.Player
                });
            }
            
            progressionManager.IncreaseLevel();
            squads.AddRange(progressionManager.GetEnemySquadsForCurrentLevel());
            
            var battleManager = Toolbox.Get<BattleManager>();
            battleManager.SetSquadsForCombat(squads);
            OnProductionsWritten?.Invoke();
        }
    }
}