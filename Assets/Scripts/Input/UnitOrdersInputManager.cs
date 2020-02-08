using UnityEngine;
using System.Collections;
using System;
using Fakejam.Players;

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
                if(targetSquad && targetSquad.owner == PlayerType.Player)
                {
                    Debug.Log("Selecting Player Unit For Orders", targetSquad);
                    sourceSquad = targetSquad;
                    return;
                }
            }
            else
            {
                
                Debug.Log($"Assigning Flag As Target for {sourceSquad.name}", clickedFlag);
                sourceSquad.setTarget(clickedFlag.Targetable);
                sourceSquad = null;
            }
        }

        public void SurfaceClicked( CombatControlSurface clickedSurface )
        {
            Debug.Log("Surface Clicked");
            if(sourceSquad != null)
            {
                Debug.Log("Assigning Surface As Target", clickedSurface);
                sourceSquad.setTarget(clickedSurface.Targetable);
                sourceSquad = null;
            }
        }

    }
}