using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fakejam.Input;

public class EnemySquadManager : MonoBehaviour
{    
    private float _currentTimeLeft;

    public float percentChanceToChangeATarget;
    public float minDecisionSeconds;
    public float maxDecisionSeconds;

    
    private void Update()
    {
        if (_currentTimeLeft <= 0)
        {
            // Timer is not turned on yet.
            return;
        }

        var deltaTime = Time.deltaTime;
        _currentTimeLeft -= deltaTime;

        if (_currentTimeLeft <= 0)
        {
            if(Random.Range(0.0f,1.0f) < percentChanceToChangeATarget)
            {
                chooseNewTarget();
            }
            
            resetTimer();
        }
    }

    public void resetTimer()
    {
        _currentTimeLeft = Random.Range(minDecisionSeconds, maxDecisionSeconds);
    }

    private void chooseNewTarget()
    {
        Toolbox.Get<InputManager>().CombatSceneManager.assignEnemySquadATarget();
    }

}
