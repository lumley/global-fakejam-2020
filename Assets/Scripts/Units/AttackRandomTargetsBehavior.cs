
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
            var targetsInRange = enemyTargets.Count;
            if (targetsInRange == 0)
            {
                return;
            }

            List<int> indexes = getIndexesToShoot(_targetCount, targetsInRange);

            for (int i = 0; i < indexes.Count; i++)
            {
                
                thisUnit.Shoot(enemyTargets[i]);
            }
        }

        private List<int> getIndexesToShoot(int numShots, int numValidTargets)
        {
            List<int> indexList = new List<int>();
            int validShots = Mathf.Min(numShots, numValidTargets);
            int numTries = validShots * 3;

            while(indexList.Count < validShots && numTries > 0)
            {
                int randIndex = Random.Range(0, validShots);
                if (!indexList.Contains(randIndex))
                {
                    indexList.Add(randIndex);
                }
                numTries--;
            }

            return indexList;
        }
    }
}