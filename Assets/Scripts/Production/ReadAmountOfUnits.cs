using Fakejam.Input;
using Fakejam.Players;
using Fakejam.Units;
using TMPro;
using UnityEngine;

namespace Fakejam.Production
{
    public class ReadAmountOfUnits : MonoBehaviour
    {
        [SerializeField] private TMP_Text _targetText;

        public void Read()
        {
            var battleManager = Toolbox.Get<BattleManager>();
            var squadsForOwner = battleManager.GetSquadsForOwner(PlayerType.Player);
            int totalCount = 0;
            
            foreach (var squad in squadsForOwner)
            {
                totalCount += squad.Count;
            }

            _targetText.text = $"You now have {totalCount} units";
        }
    }
}