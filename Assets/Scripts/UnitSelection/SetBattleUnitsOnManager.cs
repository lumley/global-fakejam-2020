using System;
using System.Collections.Generic;
using Fakejam.Input;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.Events;

namespace Fakejam.UnitSelection
{
    public class SetBattleUnitsOnManager : MonoBehaviour
    {
        [SerializeField] private ReadListOfUnits[] _readListOfUnits;

        public UnityEvent OnRun;

        private void OnEnable()
        {
            var battleManager = Toolbox.Get<BattleManager>();
            var allSquadsInCombat = battleManager.GetAllSquadsInCombat();
            foreach (var readList in _readListOfUnits)
            {
                readList.SetSquads(allSquadsInCombat);
            }
        }

        public void Run()
        {
            var battleManager = Toolbox.Get<BattleManager>();
            
            List<Squad> allSquads = new List<Squad>();
            foreach (var readList in _readListOfUnits)
            {
                allSquads.AddRange(readList.GetCurrentSquads());
            }
            
            battleManager.SetSquadsForCombat(allSquads);
            OnRun?.Invoke();
        }
    }
}