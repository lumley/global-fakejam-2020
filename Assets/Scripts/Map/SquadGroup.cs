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

        [SerializeField]
        private GameObject navMeshContainer;

        public void Spawn( UnitDefinition unitType)
        {
            this.unitType = unitType;

        }

        private void createSquadMembers()
        {
            squadMembers = new List<SquadMember>();
            for (int i = 0; i < unitType.SquadSize; i++)
            {
                SquadMember newMember = Instantiate<SquadMember>(unitType.PrefabOfUnit, navMeshContainer.transform);
                squadMembers.Add(newMember);
            }
        }

        protected override void Start()
        {
            base.Start();
            combatTarget = null;
        }

        public void setTarget(UnitTargetable target )
        {
            Debug.Log($"Target set: {target.name}", target);
            combatTarget = target;
        }
    }
}