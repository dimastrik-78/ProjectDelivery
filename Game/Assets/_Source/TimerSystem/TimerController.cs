using System.Collections;
using SignalSystem;
using UnityEngine;

namespace TimerSystem
{
    public class TimerController
    {
        private int _time;
        
        public TimerController(int time)
        {
            _time = time;
        }

        public IEnumerator Check()
        {
            while (!(_time < 0))
            {
                Signals.Get<UpdateTimeSignal>().Dispatch(_time);
                _time--;
                yield return new WaitForSeconds(1f);
            }
            Debug.Log("Game Over");
        }
    }
}