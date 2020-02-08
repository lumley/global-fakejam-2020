using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fakejam.Units;

namespace Fakejam.Input
{
    // A group of Squad Members that can be 
    public class SquadGroup : UnitTargetable
    {
        
        private UnitDefinition unitType;
        private UnitTargetable combatTarget;
        private List<SquadMember> squadMembers;

        public void Spawn( UnitDefinition definition )
        {

        }
        protected override void Start()
        {
            base.Start();
            combatTarget = null;
            squadMembers = new List<SquadMember>();
        }

        public void setTarget(UnitTargetable target )
        {
            Debug.Log($"Target set: {target.name}", target);
            combatTarget = target;
        }
    }
}