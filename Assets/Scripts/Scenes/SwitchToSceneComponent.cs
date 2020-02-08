using Fakejam.Input;
using UnityEngine;

namespace Fakejam.Scenes
{
    public class SwitchToSceneComponent : MonoBehaviour
    {
        [SerializeField] private SceneSwitchingManager.SceneType _switchTo;

        public void Switch()
        {
            var sceneSwitchingManager = Toolbox.Get<SceneSwitchingManager>();
            sceneSwitchingManager.SwitchToScene(_switchTo);
        }
    }
}