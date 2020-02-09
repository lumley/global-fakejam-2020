using Fakejam.Players;
using Fakejam.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fakejam.UnitSelection
{
    public class SquadUiElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _unitName;
        [SerializeField] private Image _unitIcon;
        [SerializeField] private TMP_InputField _unitAmount;
        private Squad _squad;

        public void SetSquad(Squad squad)
        {
            _unitName.text = squad.UnitDefinition.name;
            _unitAmount.text = squad.Count.ToString();
            _unitAmount.interactable = false; // squad.Owner == PlayerType.Player;
            var unitDefinitionIcon = squad.UnitDefinition.Icon;
            if (unitDefinitionIcon != null)
            {
                _unitIcon.enabled = true;
                _unitIcon.sprite = unitDefinitionIcon;
            }
            else
            {
                _unitIcon.enabled = false;
            }

            _squad = squad.Clone();
        }

        public Squad GetSquad()
        {
            var unitAmountText = _unitAmount.text;
            if (int.TryParse(unitAmountText, out int value))
            {
                _squad.Count = value;
            }

            return _squad;
        }
    }
}