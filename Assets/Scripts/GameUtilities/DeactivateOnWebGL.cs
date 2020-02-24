using UnityEngine;

namespace Fakejam.GameUtilities
{
    public class DeactivateOnWebGL : MonoBehaviour
    {
        [SerializeField] private bool _shouldDeactivateInEditor = true;
        
        private void Start()
        {
#if UNITY_WEBGL
    #if UNITY_EDITOR
            if (_shouldDeactivateInEditor)
            {
    #endif
                gameObject.SetActive(false);
    #if UNITY_EDITOR
            }
    #endif
#endif
        }
    }
}