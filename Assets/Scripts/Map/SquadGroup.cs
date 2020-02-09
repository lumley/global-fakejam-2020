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
        private CircleCollider2D influenceRange;

        [SerializeField]
        private GameObject influenceContainer;

        public void spawnMembers(UnitDefinition unit, int numMembers)
        {
            unitType = unit;
            influenceContainer.transform.localScale = new Vector3(unitType.InfluenceRange, unitType.InfluenceRange, 1f);

            CombatSceneManager combatManager = Toolbox.Get<InputManager>().CombatSceneManager;

            squadMembers = new List<SquadMember>();
            for (int i = 0; i < numMembers; i++)
            {
                SquadMember newMember = Instantiate(unitType.PrefabOfUnit, combatManager.squadMemberContainer.transform);
                squadMembers.Add(newMember);

                newMember.setOwner(owner);
                newMember.TeleportTo(getRandomPositionInBounds(transform.position, influenceRange.radius));
            }
        }

        private void Awake()
        {
            combatTarget = null;
        }

        public void setTarget(UnitTargetable target)
        {
            Debug.Log($"Target set: {target.name}", target);
            combatTarget = target;
            transform.position = combatTarget.transform.position;
            foreach (var member in squadMembers)
            {
                member.setTargetPos(getRandomPositionInBounds(transform.position, influenceRange.radius));
            }
            
        }

        public Vector3 getRandomPositionInBounds(Vector3 center, float mag) {
            return new Vector3(
                        Random.Range(center.x - mag/2, center.x + mag/2),
                        Random.Range(center.y - mag/2, center.y + mag/2),
                        1f);
        }
    }
}