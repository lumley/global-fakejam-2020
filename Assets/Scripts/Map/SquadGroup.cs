using System;
using UnityEngine;
using System.Collections.Generic;
using Fakejam.Units;
using Fakejam.Players;
using Units;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Fakejam.Input
{
    // A group of Squad Members that can be 
    public class SquadGroup : UnitTargetable
    {
        private UnitDefinition unitType;
        private UnitTargetable combatTarget;
        private List<SquadMember> squadMembers;
        private List<GameObject> corpses;
        public SquadInfluence influence;

        public SquadEvent OnSquadDied;

        public UnitDefinition UnitType => unitType;

        public void spawnMembers(PlayerType owner, UnitDefinition unit, int numMembers)
        {
            this.owner = owner;
            influence.setColorToOwner(this.owner);
            UnitControlFlag unitFlag = controlSurface as UnitControlFlag;
            if(!!unitFlag)
            {
                
            }
            controlSurface.gameObject.SetActive(this.owner == PlayerType.Player);
            unitType = unit;
            influence.transform.localScale = new Vector3(unitType.InfluenceRange, unitType.InfluenceRange, 1f);

            CombatSceneManager combatManager = Toolbox.Get<InputManager>().CombatSceneManager;

            squadMembers = new List<SquadMember>();
            corpses = new List<GameObject>();
            for (int i = 0; i < numMembers; i++)
            {
                SquadMember newMember = Instantiate(unitType.PrefabOfUnit, combatManager.squadMemberContainer.transform);
                squadMembers.Add(newMember);
                newMember.OnUnitDied.AddListener(OnSquadMemberDied);

                newMember.setOwner(this.owner);
                newMember.TeleportTo(getRandomPositionInBounds(transform.position, influence.Zone.radius));
                newMember.name =
                    (owner == PlayerType.Player ? "PM-" : "EM-") +
                    (unitType.PrefabOfUnit.name);
            }
        }

        
        private void OnSquadMemberDied(UnitController unitController)
        {
            for (var i = squadMembers.Count - 1; i >= 0; i--)
            {
                var squadMember = squadMembers[i];
                if (squadMember.UnitController == unitController)
                {
                    squadMembers.RemoveAt(i);
                    break;
                }
            }

            if (squadMembers.Count == 0)
            {
                gameObject.SetActive(false);
                OnSquadDied?.Invoke(this);
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
                member.setTargetPos(getRandomPositionInBounds(transform.position, influence.Zone.radius));
            }
            
        }

        public void setTargetVec(Vector3 targetVec)
        {
            
            combatTarget = null;
            transform.position = targetVec;
            foreach (var member in squadMembers)
            {
                member.setTargetPos(getRandomPositionInBounds(transform.position, influence.Zone.radius));
            }

        }

        public Vector3 getRandomPositionInBounds(Vector3 center, float mag) {
            return new Vector3(
                        Random.Range(center.x - mag/2, center.x + mag/2),
                        Random.Range(center.y - mag/2, center.y + mag/2),
                        1f);
        }

        public int GetUnitCount()
        {
            return squadMembers.Count;
        }
        
        [Serializable]
        public class SquadEvent : UnityEvent<SquadGroup>
        {
            
        }
    }
}