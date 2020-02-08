using System.Collections.Generic;
using Fakejam.Players;
using Fakejam.Units;
using UnityEngine;

namespace Fakejam.UnitSelection
{
    public class ReadListOfUnits : MonoBehaviour
    {
        [SerializeField] private PlayerType _playerType;

        [SerializeField] private Transform _transformWhereToSpawn;

        [SerializeField] private SquadUiElement _squadUiPrefab;

        private List<SquadUiElement> _spawnedElements = new List<SquadUiElement>();

        public void SetSquads(IReadOnlyList<Squad> squads)
        {
            foreach (var squad in squads)
            {
                if (squad.Owner == _playerType)
                {
                    var squadUiElement = Instantiate(_squadUiPrefab, _transformWhereToSpawn);
                    squadUiElement.SetSquad(squad);
                    _spawnedElements.Add(squadUiElement);
                }
            }
        }

        public List<Squad> GetCurrentSquads()
        {
            List<Squad> squads = new List<Squad>();
            foreach (var squadUiElement in _spawnedElements)
            {
                var squad = squadUiElement.GetSquad();
                squads.Add(squad);
            }

            return squads;
        }

    }
}