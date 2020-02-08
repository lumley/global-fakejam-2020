using UnityEngine;

namespace Fakejam.GameUtilities
{
    public class ToggleGameObjectActive : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        
        public void Toggle()
        {
            if (_target != null)
            {
                _target.SetActive(!_target.activeSelf);
            }
        }
    }
}