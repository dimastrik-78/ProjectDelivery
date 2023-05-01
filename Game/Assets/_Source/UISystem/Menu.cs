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
        
        }

        public void MusicOff()
        {
            audioMixer.SetFloat("Music", -80f);
            PlayerPrefs.SetFloat("MusicOff", sliderMusics.value);
        }
        
        public void SoundOn()
        {
            sliderEffects.value = PlayerPrefs.GetFloat("EffectsOff");
        }

        public void MusicOn()
        {
            sliderMusics.value = PlayerPrefs.GetFloat("MusicOff");
        }
    }
}
