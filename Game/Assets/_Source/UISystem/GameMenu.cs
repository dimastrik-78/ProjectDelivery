using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UISystem
{
   public class GameMenu : MonoBehaviour
   {
      [SerializeField] private GameObject panelSettings;
      [SerializeField] private Button panel;
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
         panel.onClick.AddListener(OnMenu);
      }
      public void OnMenu()
      {
         panelSettings.SetActive(true);
         Time.timeScale = 0;
         panel.onClick.RemoveListener(OnMenu);
         panel.onClick.AddListener(Back);
      }
   
      public void Back()
      {
         PlayerPrefs.SetFloat("Music", sliderMusics.value);
         PlayerPrefs.SetFloat("Effects", sliderEffects.value);
         panelSettings.SetActive(false);
         Time.timeScale = 1;
         panel.onClick.RemoveListener(Back);
         panel.onClick.AddListener(OnMenu);
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
         {
            sliderMusics.value = PlayerPrefs.GetFloat("MusicOff");
         }
      }
   }
}
