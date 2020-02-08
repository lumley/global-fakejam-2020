using System.Collections;
using System.Collections.Generic;
using Fakejam.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fakejam.Input
{
    [RequireComponent(typeof(CombatClickable))]
    public class CombatControlSurface : MonoBehaviour
    {
        /// <summary>
        /// A Control Surface is any item that can be interacted with, or set as a Unit's target
        /// Map Tiles
        /// Other Squads
        /// Objectives
        ///     
        /// </summary>

        private CombatClickable combatClickable;

        private UnitTargetable targetable;
        public UnitTargetable Targetable => targetable;

        public void init(UnitTargetable targetable)
        {
            this.targetable = targetable;
        
            combatClickable = GetComponent<CombatClickable>();
            combatClickable.OnClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            if(combatClickable != null)
            {
                combatClickable.OnClick.RemoveListener(OnClick);
            }
        }

        protected virtual void OnClick()
        {
            // Override this function for click functionality
            Debug.Log("Clicker");
            inputManager.CombatInputManager.OnSurfaceClicked(this);
        }


        // Helper for getting input manager for OnClick (and overrides)
        protected InputManager inputManager => Toolbox.Get<InputManager>();
                    
    }
}