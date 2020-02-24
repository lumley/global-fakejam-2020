using System.Collections.Generic;
using Fakejam.Input;
using Fakejam.Players;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Fakejam.Production
{

    public class RandomizeProgression : MonoBehaviour
    {
        public UnityEvent OnRandomizeProgression;

        public void RandomizeIt()
        {
            var progressionManager = Toolbox.Get<ProgressionManager>();
            progressionManager.Restart();

            var battleManager = Toolbox.Get<BattleManager>();

            var allSquads = new List<Squad>();
            foreach (UnitDefinition unit in battleManager.AllUnitDefinitions)
            {
                allSquads.Add(CreateSquadFor(PlayerType.Enemy1, unit));
                allSquads.Add(CreateSquadFor(PlayerType.Player, unit));
            }

            battleManager.SetSquadsForCombat(allSquads);

            OnRandomizeProgression?.Invoke();
        }

        public Squad CreateSquadFor(PlayerType ownerType, UnitDefinition unit)
        {
            return new Squad
            {
                Owner = ownerType,
                UnitDefinition = unit,
                Count = Random.Range(0, unit.SquadSize * 2)
            };
        }

    }
}

    