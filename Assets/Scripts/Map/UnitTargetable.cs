using UnityEngine;
using System.Collections;
using Fakejam.Input;

public class UnitTargetable : CombatMapEntity
{
    
    public CombatControlSurface controlSurface;

    protected virtual void Start()
    {
        controlSurface.init(this);
    }
}
