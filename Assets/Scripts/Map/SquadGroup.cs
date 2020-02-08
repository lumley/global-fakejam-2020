using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fakejam.Input
{
    // A group of Squad Members that can be 
    public class SquadGroup : UnitTargetable
    {
        
        private UnitTargetable combatTarget;
        private List<SquadMember> squadMembers;

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