using SignalSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TimerSystem
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Awake()
        {
            Signals.Get<UpdateTimeUISignal>().AddListener(ChangeTimer);
        }

        private void ChangeTimer(int time)
        {
            text.text = time.ToString();
        }
    }
}