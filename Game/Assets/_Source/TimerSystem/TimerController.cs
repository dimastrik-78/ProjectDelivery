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

        private void AddTime(int addTime) => _time += addTime;

        private void RemoveTime(int removeTime) => _time -= removeTime;

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