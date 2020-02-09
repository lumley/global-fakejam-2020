using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Fakejam.Units
{
    [CreateAssetMenu(fileName = nameof(AttackRandomTargetsBehavior), order = 0, menuName = AssetMenuConstants.ScriptableObjectsMenu + nameof(AttackRandomTargetsBehavior))]
    public class AttackRandomTargetsBehavior : AttackBehavior
    {
        [SerializeField] private int _targetCount = 1;
        
        public override void Attack(UnitController thisUnit, IList<UnitController> enemyTargets)
        {
            var enemyTargetsCount = enemyTargets.Count;
            if (enemyTargetsCount == 0)
            {
                return;
            }
            
            var startingEnemy = Random.Range(0, enemyTargetsCount);
            for (int i = 0; i < _targetCount; i++)
            {
                int index = (i + startingEnemy) % enemyTargetsCount;
                var unitController = enemyTargets[index];
                thisUnit.Shoot(unitController);
            }
        }
    }
}