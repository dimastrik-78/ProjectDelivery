using TimerSystem;
using UnityEngine;

namespace core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private int time;
        
        private void Start()
        {
            StartCoroutine(new TimerController(time).Check());
        }
    }
}
