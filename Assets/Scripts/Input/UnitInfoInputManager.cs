using UnityEngine;
using System.Collections;
using Fakejam.Input;

public class UnitInfoInputManager : MonoBehaviour
{
    private UnitTargetable selectedInfoTarget;

    public bool Deselect()
    {
        if (selectedInfoTarget)
        {
            selectedInfoTarget = null;
            return true;
        }
        return false;
    }

    public void SurfaceClicked(CombatControlSurface clickedSurface)
    {
        selectedInfoTarget = clickedSurface.Targetable;
    }
}
