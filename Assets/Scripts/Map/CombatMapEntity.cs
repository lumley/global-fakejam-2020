using UnityEngine;
using System.Collections;
using System.Drawing;
using Fakejam.Players;

namespace Fakejam.Input
{
    /// <summary>
    /// An entity that exists on the map
    /// Might be clickable, might not.
    /// Examples: comabt group
    /// 
    /// 
    /// </summary>

    public class CombatMapEntity : MonoBehaviour
    {
        [SerializeField]
        public PlayerType owner;

        [SerializeField]
        public Point gridPos;

    }
}