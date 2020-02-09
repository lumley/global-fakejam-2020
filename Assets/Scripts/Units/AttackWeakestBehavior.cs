using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Fakejam.Units
{
    [CreateAssetMenu(fileName = nameof(AttackWeakestBehavior), order = 0, menuName = AssetMenuConstants.ScriptableObjectsMenu + nameof(AttackWeakestBehavior))]
    public class AttackWeakestBehavior : AttackBehavior
    {
        public override void Attack(UnitController thisUnit, IList<UnitController> enemyTargets)
        {
            if (enemyTargets.Count == 0)
            {
                return;
            }

            UnitController weakestEnemy = null;

            foreach (var unitController in enemyTargets)
            {
                if (weakestEnemy == null || weakestEnemy.Health > unitController.Health)
                {
                    weakestEnemy = unitController;
                }
            }

            if (weakestEnemy != null)
            {
                weakestEnemy.TakeDamage(thisUnit.UnitDefinition.Damage);
            }
        }
    }
}