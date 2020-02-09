using Fakejam.Input;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Fakejam.Production
{
    public class ResetProgression : MonoBehaviour
    {
        public UnityEvent OnProgressionReset;
        
        public void Reset()
        {
            var progressionManager = Toolbox.Get<ProgressionManager>();
            var battleManager = Toolbox.Get<BattleManager>();
            
            progressionManager.Restart();
            var enemySquadsForCurrentLevel = progressionManager.GetEnemySquadsForCurrentLevel();
            battleManager.SetSquadsForCombat(enemySquadsForCurrentLevel);
            
            OnProgressionReset?.Invoke();
        }
    }
}