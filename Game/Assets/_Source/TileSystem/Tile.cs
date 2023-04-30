using TileSystem.Data;
using UnityEngine;

namespace TileSystem
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileSO tileSO;

        public TileSO GetTileSO() => tileSO;
    }
}