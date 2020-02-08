using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    
    public class TerrainTile
    {
        private int x;
        private int y;
        private string terrain;
    }
    public class MapController : MonoBehaviour
    {

        [SerializeField]
        private Tilemap tilemap;

        [SerializeField] private int sizeX;

        [SerializeField] private int sizeY;

        [SerializeField] private CombatTerrainTile grass;

        [SerializeField] private CombatTerrainTile mountain;

        [SerializeField] private CombatTerrainTile woods;

      
        void Start()
        {

           
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    
                    //tilemap.SetTile(new Vector3Int(x,y,0), grass);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

    }
}
