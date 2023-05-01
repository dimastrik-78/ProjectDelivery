using System.Collections;
using SignalSystem;
using UnityEngine;

namespace TimerSystem
{
    public class TimerController
    {
        private readonly int _addTime;
        private readonly int _removeTime;
        
        private int _time;

        public TimerController(int time, int addTime, int removeTime)
        {
            _time = time;
            _addTime = addTime;
            _removeTime = removeTime;
            
            Subscription();
        }
        
        private void Subscription()
        {
            Signals.Get<AddTimeSignal>().AddListener(AddTime);
            Signals.Get<RemoveTimeSignal>().AddListener(RemoveTime);
        }

        private void Repudiation()
        {
            Signals.Get<AddTimeSignal>().RemoveListener(AddTime);
            Signals.Get<RemoveTimeSignal>().RemoveListener(RemoveTime);
        }

        private void AddTime()
        {
            _time += _addTime;
            Signals.Get<UpdateTimeUISignal>().Dispatch(_time);
        }

        private void RemoveTime()
        {
            _time -= _removeTime;
            Signals.Get<UpdateTimeUISignal>().Dispatch(_time);
        }

        public IEnumerator Check()
        {
            while (!(_time < 0))
            {
                Signals.Get<UpdateTimeUISignal>().Dispatch(_time);
                _time--;
                yield return new WaitForSeconds(1f);
            }
            Debug.Log("Game Over");
            Repudiation();
        }

        
    }
}