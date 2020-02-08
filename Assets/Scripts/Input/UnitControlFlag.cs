using UnityEngine;
using System.Collections;
using Fakejam.Input;

/// <summary>
/// A special type of Control Surface.
/// All control surfaces can be a target
///
/// Control Flags represent a group of items that take behavior
/// Squads and Objective have control flags
/// Map tiles only have Control Surfaces
///
/// 
/// </summary>


public class UnitControlFlag : CombatControlSurface
{
    
    protected override void OnClick()
    {
        Debug.Log("Flag Click");
        var man = inputManager;
        var com = man.CombatInputManager;
        com.OnControlFlagClicked(this);
    }

}
