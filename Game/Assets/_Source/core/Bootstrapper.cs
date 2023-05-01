using LevelSystem;
using TileSystem;
using TimerSystem;
using UnityEngine;

namespace core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Header("Timer System")]
        [SerializeField] private int time;
        [SerializeField] private int addTime;
        [SerializeField] private int removeTime;

        [Header("Level System")] 
        [SerializeField] private GameObject firstTile;
        [SerializeField] private GameObject transform;
        [SerializeField] private Transform point;
        [SerializeField] private int countSpawnTile;
        [SerializeField] private Transform center;
        
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            StartCoroutine(new TimerController(time, addTime, removeTime).Check());
            new Generation(transform).InstTiles(new ObjectPool(point), firstTile.transform.GetChild(0).GetComponent<Tile>(), countSpawnTile, center);
        }
    }
}
