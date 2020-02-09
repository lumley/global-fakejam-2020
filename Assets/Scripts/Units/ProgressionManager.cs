using System;
using UnityEngine;

namespace Fakejam.Units
{
    public class ProgressionManager : MonoBehaviour
    {
        [SerializeField] private CombatLevel[] _combatLevels;
        [SerializeField] private float[] _productionTimeLevels;

        [SerializeField] private int _currentLevel;

        public void IncreaseLevel()
        {
            _currentLevel = Math.Min(_currentLevel + 1,  _combatLevels.Length);
        }

        public Squad[] GetEnemySquadsForCurrentLevel()
        {
            return _combatLevels[_currentLevel].Enemies;
        }

        public float GetProductionTimeForCurrentLevel()
        {
            return _productionTimeLevels[_currentLevel];
        }

        [Serializable]
        private class CombatLevel
        {
            [SerializeField] private Squad[] _enemies;

            public Squad[] Enemies => _enemies;
        }
    }
}