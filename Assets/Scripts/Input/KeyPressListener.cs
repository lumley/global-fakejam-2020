using UnityEngine;

namespace Fakejam.Input
{
    public class KeyPressListener : MonoBehaviour
    {
        [SerializeField] private InputManager.KeyType _listenToKey;
        
        public InputManager.KeyTypeEvent OnKeyPressed;
        private InputManager _inputManager;

        private void OnEnable()
        {
            _inputManager = Toolbox.Get<InputManager>();
            _inputManager.OnKeyPressed.AddListener(OnAnyKeyPressed);
        }

        private void OnAnyKeyPressed(InputManager.KeyType keyType)
        {
            if (keyType == _listenToKey)
            {
                OnKeyPressed?.Invoke(_listenToKey);
            }
        }

        private void OnDisable()
        {
            if (_inputManager != null)
            {
                _inputManager.OnKeyPressed.RemoveListener(OnAnyKeyPressed);
            }
        }
        
    }
}