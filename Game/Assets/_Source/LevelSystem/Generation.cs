using UnityEngine;
using TileSystem;
using TileSystem.Data;

namespace LevelSystem
{
    public class Generation
    {
        private readonly System.Random _random = new();

        public void InstTiles(ObjectPool pool, Tile tile, int countSpawn)
        {
            TileSO tileSO = tile.GetTileSO();
            for (int i = 0; i < countSpawn; i++)
            {
                pool.AddTile(Object.Instantiate(tileSO.Tile));
                pool.TileMoving();

                tileSO = tileSO.AttachableTiles[_random.Next(0, tileSO.AttachableTiles.Length)].GetComponent<Tile>()
                    .GetTileSO();
            }
        }
    }
}