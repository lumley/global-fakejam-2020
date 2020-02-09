using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fakejam.Input;

public class EnemySquadManager : MonoBehaviour
{    
    private float _currentTimeLeft;

    public float chanceToChangeTarget;
    public float minDecisionTime;
    public float maxDecisionTime;

    
    private void Update()
    {
        if (_currentTimeLeft <= 0)
        {
            // Timer is not turned on yet.
            return;
        }

        var deltaTime = Time.deltaTime;
        _currentTimeLeft -= deltaTime;

        Debug.Log($"time: { _currentTimeLeft}");
        if (_currentTimeLeft <= 0)
        {
            if(Random.Range(0.0f,1.0f) < chanceToChangeTarget)
            {
                chooseNewTarget();
            }
            
            resetTimer();
        }
    }

    public void resetTimer()
    {
        _currentTimeLeft = Random.Range(minDecisionTime, maxDecisionTime);
    }

    private void chooseNewTarget()
    {
        Toolbox.Get<InputManager>().CombatSceneManager.assignEnemySquadATarget();
    }

}
