using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider sliderEffects;
        [SerializeField] private Slider sliderMusics;
        [SerializeField] private Button buttonMusic;
        [SerializeField] private Button buttonEffects;
        
        void Awake()
        {
            if (PlayerPrefs.HasKey("Music")
                && PlayerPrefs.HasKey("Effects"))
            {
                sliderEffects.value = PlayerPrefs.GetFloat("Effects");
                sliderMusics.value = PlayerPrefs.GetFloat("Music");
            }
            else
            {
                PlayerPrefs.SetFloat("Music", sliderMusics.value);
                PlayerPrefs.SetFloat("Effects", sliderEffects.value);
            }
            Debug.Log(sliderMusics.value);
            buttonMusic.onClick.AddListener(SoundOff);
            buttonEffects.onClick.AddListener(SoundOff);
        }

        public void Play()
        {
            PlayerPrefs.Save();

            SceneManager.LoadScene(1);
        }

        public void Settings()
        {
            menuPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }

        public void Back()
        {
            PlayerPrefs.SetFloat("Music", sliderMusics.value);
            PlayerPrefs.SetFloat("Effects", sliderEffects.value);

            menuPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }

        public void ChangeSound()
        {
            audioMixer.SetFloat("Music", sliderMusics.value);
            audioMixer.SetFloat("Effects", sliderEffects.value);

        }

        public void SoundOff()
        {
            audioMixer.SetFloat("Effects", -80f);
            PlayerPrefs.SetFloat("EffectsOff", sliderEffects.value);
            buttonEffects.onClick.AddListener(SoundOn);
            buttonEffects.onClick.RemoveListener(SoundOff);
        }

        public void MusicOff()
        {
            audioMixer.SetFloat("Music", -80f);
            PlayerPrefs.SetFloat("MusicOff", sliderMusics.value);
            buttonMusic.onClick.RemoveListener(SoundOff);
            buttonMusic.onClick.AddListener(MusicOn);
        }
        
        private void SoundOn()
        {
            sliderEffects.value = PlayerPrefs.GetFloat("EffectsOff");
            buttonEffects.onClick.AddListener(SoundOff);
            buttonEffects.onClick.RemoveListener(SoundOn);
        }

        private void MusicOn()
        {
            sliderMusics.value = PlayerPrefs.GetFloat("MusicOff");
            buttonMusic.onClick.RemoveListener(MusicOn);
            buttonMusic.onClick.AddListener(SoundOff);
        }
    }
}
