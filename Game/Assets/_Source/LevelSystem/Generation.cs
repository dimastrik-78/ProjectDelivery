using System.Collections.Generic;
using UnityEngine;
using TileSystem;
using TileSystem.Data;
using UnityEditor;

namespace LevelSystem
{
    public class Generation
    {
        private readonly System.Random _random = new();

        private GameObject _point;
        private List<GameObject> _list = new List<GameObject>();

        public Generation(GameObject point)
        {
            _point = point;
        }

        public void InstTiles(ObjectPool pool, Tile tile, int countSpawn, Transform center)
        {
            TileSO tileSO = tile.GetTileSO();
            for (int i = 0; i < countSpawn; i++)
            {
                tileSO = tileSO.AttachableTiles[_random.Next(0, tileSO.AttachableTiles.Length)].transform.GetChild(0).GetComponent<Tile>()
                    .GetTileSO();
                // var l = Object.Instantiate(tileSO.Tile).transform.GetChild(0).GetComponent<Tile>().GetSpawnPoint();
                // TileMoving(tileSO.Tile.transform.GetChild(0).GetComponent<Tile>().GetSpawnOtherTilePoint(), 
                //     l);
                _list.Add(Object.Instantiate(tileSO.Tile));
            }
        }
    }
}