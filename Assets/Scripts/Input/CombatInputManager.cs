using UnityEngine;

namespace Fakejam.Input
{
    [RequireComponent(typeof(UnitOrdersInputManager))]
    [RequireComponent(typeof(UnitInfoInputManager))]
    public class CombatInputManager : MonoBehaviour
    {
        private UnitOrdersInputManager unitOrdersInputManager;
        private UnitInfoInputManager unitInfoInputManager;

        private void Start()
        {
            unitOrdersInputManager = GetComponent<UnitOrdersInputManager>();
            unitInfoInputManager = GetComponent<UnitInfoInputManager>();
            Toolbox.Get<InputManager>().CombatInputManager = this;
        }

        public void onDeselect() {
            // first deselect the flag input
            if(unitOrdersInputManager.Deselect())
            {
                return;
            }

            // then try the unit info
            if ( unitInfoInputManager.Deselect() )
            {
                return;
            }
        }

        public void OnControlFlagClicked(UnitControlFlag clickedFlag)
        {   
            if (!unitOrdersInputManager.isWaitingForTarget)
            {
                // Dont show UnitInfo if we are currently looking for a Target for UnitOrders
                unitInfoInputManager.SurfaceClicked(clickedFlag);
            }

            unitOrdersInputManager.ControlFlagClicked(clickedFlag);
        }

        public void OnSurfaceClicked(CombatControlSurface targetSurface)
        {
            unitOrdersInputManager.SurfaceClicked( targetSurface );
        }
    }
}