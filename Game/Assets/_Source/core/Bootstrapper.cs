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
        [SerializeField] private Tile firstTile;
        [SerializeField] private Transform point;
        [SerializeField] private int countSpawnTile;
        
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            StartCoroutine(new TimerController(time, addTime, removeTime).Check());
            // new Generation().InstTiles(new ObjectPool(point), firstTile, countSpawnTile);
        }
    }
}
