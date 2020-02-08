using UnityEngine;
using System.Collections;
using Fakejam.Input;

[RequireComponent(typeof(UnitControlFlag))]
public class UnitTargetable : CombatMapEntity
{
    [SerializeField]
    private UnitControlFlag controlFlag;

    void Start()
    {
        controlFlag = GetComponent<UnitControlFlag>();
        controlFlag.init(this);
    }
}
