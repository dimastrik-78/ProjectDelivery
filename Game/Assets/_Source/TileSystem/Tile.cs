using TileSystem.Data;
using UnityEngine;

namespace TileSystem
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileSO tileSO;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform spawnOtherTilePoint;

        public TileSO GetTileSO() => tileSO;
        public Transform GetSpawnPoint() => spawnPoint;
        public Transform GetSpawnOtherTilePoint() => spawnOtherTilePoint;
    }
}