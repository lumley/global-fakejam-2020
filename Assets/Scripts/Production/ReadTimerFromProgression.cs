using System;
using Fakejam.Input;
using Fakejam.Units;
using TMPro;
using UnityEngine;

namespace Fakejam.Production
{
    public class ReadTimerFromProgression : MonoBehaviour
    {
        [SerializeField] private CountdownTimer _countdownTimer;

        [SerializeField] private TMP_Text _timeLeftText;

        private void OnEnable()
        {
            var progressionManager = Toolbox.Get<ProgressionManager>();
            var productionTimeForCurrentLevel = progressionManager.GetProductionTimeForCurrentLevel();
            
            _countdownTimer.OnSecondPassed.AddListener(OnSecondPassed);
            _countdownTimer.SetTime(productionTimeForCurrentLevel);
        }

        private void OnDisable()
        {
            if (_countdownTimer != null)
            {
                _countdownTimer.OnSecondPassed.RemoveListener(OnSecondPassed);
            }
        }

        private void OnSecondPassed(float timeLeft)
        {
            var fromSeconds = TimeSpan.FromSeconds(timeLeft);
            _timeLeftText.text = fromSeconds.ToString("mm':'ss");
        }
    }
}