using TimerSystem;
using UnityEngine;

namespace core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private int time;
        
        void Start()
        {
            StartCoroutine(new TimerController(time).Check());
        }
    }
}
