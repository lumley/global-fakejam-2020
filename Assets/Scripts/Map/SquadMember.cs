using UnityEngine;
using Units;
using Fakejam.Players;

namespace Fakejam.Input
{
    public class SquadMember : MonoBehaviour
    {
        public Vector3 targetPos;

        [SerializeField]
        private UnitController unitController;

        public void TeleportTo(Vector3 pos)
        {
            this.transform.position = pos;
        }

        public void setTargetPos( Vector3 target)
        {
            targetPos = target;
            unitController.Move(targetPos);
        }

        public void setOwner(PlayerType type)
        {
            unitController.setOwner(type);
        }
    }
}
