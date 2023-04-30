using TimerSystem;
using UnityEngine;

namespace core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private int time;
        [SerializeField] private int addTime;
        [SerializeField] private int removeTime;
        
        private void Start()
        {
            StartCoroutine(new TimerController(time, addTime, removeTime).Check());
        }
    }
}
