using System;
using UnityEngine;

namespace Fakejam.Units
{
    public class ProgressionManager : MonoBehaviour
    {
        [SerializeField] private CombatLevel[] _combatLevels;

        [SerializeField] private int _currentLevel;

        public void IncreaseLevel()
        {
            _currentLevel = (_currentLevel + 1) % _combatLevels.Length;
        }

        public Squad[] GetEnemySquadsForCurrentLevel()
        {
            return _combatLevels[_currentLevel].Enemies;
        }

        [Serializable]
        private class CombatLevel
        {
            [SerializeField] private Squad[] _enemies;

            public Squad[] Enemies => _enemies;
        }
    }
}