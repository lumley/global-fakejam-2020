using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Fakejam.Units
{
    [CreateAssetMenu(fileName = nameof(AttackEveryoneBehavior), order = 0, menuName = AssetMenuConstants.ScriptableObjectsMenu + nameof(AttackEveryoneBehavior))]
    public class AttackEveryoneBehavior : AttackBehavior
    {
        public override void Attack(UnitController thisUnit, IList<UnitController> enemyTargets)
        {
            foreach (var unitController in enemyTargets)
            {
                unitController.TakeDamage(thisUnit.UnitDefinition.Damage);
            }
        }
    }
}