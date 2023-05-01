using SignalSystem;
using UnityEngine;

namespace AudioSystem
{
    public class Audio : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private AudioSource runAudio;
        [SerializeField] private AudioSource jumpAudio;
        [SerializeField] private AudioSource useDoorAudio;
        [SerializeField] private AudioSource damageAudio;
        [SerializeField] private AudioSource tenSecondAudio;
        [SerializeField] private AudioSource gameOverAudio;
        [SerializeField] private AudioSource soundtrackAudio;
        
        public static AudioSource RunAudio;
        public static AudioSource JumpAudio;
        public static AudioSource UseDoorAudio;
        public static AudioSource DamageAudio;
        public static AudioSource TenSecondAudio;
        public static AudioSource GameOverAudio;
        public static AudioSource SoundtrackAudio;

        private void Awake()
        {
            Signals.Get<UpdateTimeSignal>().AddListener(TimeCheck);
            RunAudio = runAudio;
            JumpAudio = jumpAudio;
            UseDoorAudio = useDoorAudio;
            DamageAudio = damageAudio;
            TenSecondAudio = tenSecondAudio;
            GameOverAudio = gameOverAudio;
            SoundtrackAudio = soundtrackAudio;
        }

        private void OnDisable()
        {
            Signals.Get<UpdateTimeSignal>().RemoveListener(TimeCheck);
        }

        private void Update()
        {
            if (rb.velocity.x != 0)
            {
                RunAudio.Play();
            }
        }

        private void TimeCheck(int time)
        {
            if (time <= 10
                && !TenSecondAudio.isPlaying)
            {
                TenSecondAudio.Play();
            }
            else
            {
                TenSecondAudio.Stop();
            }
        }
    }
}