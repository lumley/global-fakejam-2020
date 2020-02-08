using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Map
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Combat Terrain Tile", menuName = "FGJ2020/Terrain", order = 0)]
    public class CombatTerrainTile : Tile
    {

        [SerializeField]
        private float difficulty;

    }
}
