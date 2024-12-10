using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mainMixer; 
    public Slider mainVolumeSlider; 
    public Slider musicVolumeSlider; 
    public Slider sfxVolumeSlider; 

    void Start()
    {
       
        InitializeSlider(mainVolumeSlider, "MasterVolumeEx", mainMixer);

       
        InitializeSlider(musicVolumeSlider, "MusicVolumeEx", mainMixer);

      
        InitializeSlider(sfxVolumeSlider, "SFXVolumeEx", mainMixer);
    }

    private void InitializeSlider(Slider slider, string parameter, AudioMixer mixer)
    {
        if (slider == null || mixer == null) return;

       
        float savedValue = PlayerPrefs.GetFloat(parameter, 0.75f);
        slider.value = savedValue;

       
        SetVolume(mixer, parameter, savedValue);
        slider.onValueChanged.AddListener(value => SetVolume(mixer, parameter, value));
    }

    public void SetVolume(AudioMixer mixer, string parameter, float volume)
    {
        
        float volumeInDb = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1)) * 20;
        mixer.SetFloat(parameter, volumeInDb);

       
        PlayerPrefs.SetFloat(parameter, volume);
    }
}
