using System;
using UnityEngine;
using UnityEngine.Events;

namespace Fakejam.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private string cancelKeyName = "Cancel";
        
        public CombatInputManager CombatInputManager { get; set; }

        public KeyTypeEvent OnKeyPressed;

        private void Update()
        {
            if (UnityEngine.Input.GetButtonDown(cancelKeyName))
            {
                OnKeyPressed?.Invoke(KeyType.Cancel);
            }
        }
        
        public enum KeyType
        {
            Cancel,
        }
        
        [Serializable]
        public class KeyTypeEvent : UnityEvent<KeyType>
        {
            
        }
    }
}