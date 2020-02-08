using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fakejam.Units;

namespace Fakejam.Input
{
    // A group of Squad Members that can be 
    public class SquadGroup : UnitTargetable
    {
        public UnitDefinition unitType;
        private UnitTargetable combatTarget;
        private List<SquadMember> squadMembers;

        private void createSquadMembers()
        {
            BattleManager battleManager = Toolbox.Get<BattleManager>();
            squadMembers = new List<SquadMember>();
            for (int i = 0; i < unitType.SquadSize; i++)
            {
                SquadMember newMember = Instantiate(unitType.PrefabOfUnit, battleManager.combatUnitsContainer.transform);
                squadMembers.Add(newMember);

                Collider2D spawnArea = battleManager.playerSpawnArea;

                newMember.setTargetPos(new Vector3(
                    Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.size.x),
                    Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.size.y),
                    1f
                ));
            }
        }

        protected override void Start()
        {
            base.Start();
            combatTarget = null;
            createSquadMembers();
        }

        public void setTarget(UnitTargetable target )
        {
            Debug.Log($"Target set: {target.name}", target);
            combatTarget = target;
        }
    }
}