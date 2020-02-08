using UnityEngine;
using System.Collections;
using System;

namespace Fakejam.Input
{
    /// <summary>
    ///  Manages sending orders to the player's units
    /// </summary>
    /// 
    public class UnitOrdersInputManager : MonoBehaviour
    {
        private SquadGroup sourceSquad; // The squad that we are trying to issue an order to

        internal bool isWaitingForTarget => sourceSquad != null;

        // Use this for initialization
        void Start()
        {
            sourceSquad = null;
        }

        public bool Deselect( )
        {
            if(sourceSquad)
            {
                sourceSquad = null;
                return true;
            }
            return false;
        }

        public void ControlFlagClicked(UnitControlFlag clickedFlag)
        {
            if (sourceSquad == null)
            {
                var targetSquad = clickedFlag.Targetable as SquadGroup;
                if(targetSquad && targetSquad.owner == Faction.PLAYER)
                {
                    sourceSquad = targetSquad;
                    return;
                }
            }
        }

        public void SurfaceClicked( CombatControlSurface clickedSurface )
        {
            if(sourceSquad != null)
            {
                sourceSquad.setTarget(clickedSurface.Targetable);
            }
        }

    }
}