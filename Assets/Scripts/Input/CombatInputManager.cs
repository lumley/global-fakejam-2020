using UnityEngine;

namespace Fakejam.Input
{
    public class CombatInputManager : MonoBehaviour
    {
        private void Start()
        {
            var inputManager = Toolbox.Get<InputManager>();
            inputManager.CombatInputManager = this;
        }

        public void OnFlagSelected(CombatControlFlag targetFlag )
        {
            Debug.Log($"Clicked {targetFlag.name}", targetFlag);
        }


    }
}