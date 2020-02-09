using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Fakejam.Units
{
    public class AttackBehavior : ScriptableObject
    {
        public virtual void Attack(UnitController thisUnit, IList<UnitController> enemyTargets)
        {
            
        }
    }
}