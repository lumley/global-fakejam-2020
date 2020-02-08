using UnityEngine;

namespace Fakejam.Scenes
{
    public class PauseGameOnEnable : MonoBehaviour
    {
        [SerializeField] private float _timeScale = 1f;
        
        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = _timeScale;
        }
    }
}