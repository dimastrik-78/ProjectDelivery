using UnityEngine;

namespace TileSystem.Data
{
    [CreateAssetMenu(menuName = "SO/Tile", fileName = "Tile")]
    public class TileSO : ScriptableObject
    {
        [SerializeField] private GameObject tile;
        [SerializeField] private GameObject[] attachableTiles;
        [SerializeField] private int[] chance;

        public GameObject Tile => tile;
        public GameObject[] AttachableTiles => attachableTiles;
        public int[] Chance => chance;
    }
}