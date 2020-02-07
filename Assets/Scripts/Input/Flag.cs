using System.Collections;
using System.Collections.Generic;
using Fakejam.Input;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof (CombatClickable))]
public class Flag : MonoBehaviour {

    private CombatClickable combatClickable;

    private void Awake()
    {
        combatClickable = GetComponent<CombatClickable>();
        combatClickable.OnClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        combatClickable.OnClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        var inputManager = Toolbox.Get<InputManager>();
        inputManager.CombatInputManager.OnFlagSelected(this);
    }
}