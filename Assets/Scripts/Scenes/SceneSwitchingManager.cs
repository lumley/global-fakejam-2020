using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fakejam.Scenes
{
    public class SceneSwitchingManager : MonoBehaviour
    {
        [SerializeField] private SceneToIndexMap[] _sceneMappings;

        public void SwitchToScene(SceneType sceneType)
        {
            foreach (var sceneToIndexMap in _sceneMappings)
            {
                if (sceneToIndexMap.MatchingType == sceneType)
                {
                    SwitchToSceneByIndex(sceneToIndexMap.Index);
                    return;
                }
            }
        }

        private void SwitchToSceneByIndex(int index)
        {
            SceneManager.LoadScene(index);
        }

        public enum SceneType
        {
            CombatSelection,
            Combat
        }
        
        [Serializable]
        private class SceneToIndexMap
        {
            public int Index;
            public SceneType MatchingType;
        }
    }
}