using System.Collections.Generic;
using Fakejam.Players;
using UnityEngine;

namespace Fakejam.Units
{
    public class BattleManager : MonoBehaviour
    {
        [Header("All units")]
        [SerializeField] private UnitDefinition[] _allUnitDefinitions;

        [Header("Initial combat values")] [SerializeField]
        private List<Squad> _combatSquads;

        public UnitDefinition[] AllUnitDefinitions => _allUnitDefinitions;

        public List<Squad> GetAllSquadsInCombat()
        {
            return _combatSquads;
        }

        public List<Squad> GetSquadsForOwner(PlayerType ownerType)
        {
            var squadsForOwner = new List<Squad>();
            foreach (var combatSquad in _combatSquads)
            {
                if (combatSquad.Owner == ownerType)
                {
                    squadsForOwner.Add(combatSquad);
                }
            }

            return squadsForOwner;
        }

        public void SetSquadsForCombat(IReadOnlyCollection<Squad> allSquads)
        {
            _combatSquads = new List<Squad>(allSquads);
        }
    }
}