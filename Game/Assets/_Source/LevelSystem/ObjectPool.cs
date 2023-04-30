using System.Collections.Generic;
using UnityEngine;

namespace LevelSystem
{
    public class ObjectPool
    {
        private readonly List<GameObject> _tilePool = new();
        private readonly Transform _spawnPoint;
        private readonly System.Random _random = new();

        public ObjectPool(Transform spawnPoint) 
        {
            _spawnPoint = spawnPoint;
        }

        public void TileMoving()
        {
            GameObject tile = CheckPool();
            tile.transform.position = _spawnPoint.position;
            _spawnPoint.position = new Vector3(0, 0, _spawnPoint.position.z + tile.transform.localScale.z);
            tile.SetActive(true);
        }

        public void AddTile(GameObject tile)
        {
            _tilePool.Add(tile);
        }

        private GameObject CheckPool()
        {
            List<GameObject> tiles = new();

            for (int i = 0; i < _tilePool.Count; i++)
            {
                if (!_tilePool[i].activeSelf)
                {
                    tiles.Add(_tilePool[i]);
                }
            }

            if (tiles.Count != 0)
            {
                return SelectTile(tiles);
            }
            
            return Object.Instantiate(_tilePool[_random.Next(0, _tilePool.Count)]);
        }

        private GameObject SelectTile(List<GameObject> tiles) => tiles[_random.Next(0, tiles.Count)];
    }
}