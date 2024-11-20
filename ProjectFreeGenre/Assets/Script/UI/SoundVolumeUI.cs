using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeUI : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundEffectVolumeSlider;

    private void Start()
    {
        musicVolumeSlider.value = SoundManager.Instance.musicVolume;
        soundEffectVolumeSlider.value = SoundManager.Instance.soundEffectVolume;
    }

    public void SetMusicVolume(float volume)
    {
        SoundManager.Instance.SetMusicVolume(volume);
    }

    public void SetSoundEffectVolume(float volume)
    {
        SoundManager.Instance.SetSoundEffectVolume(volume);
    }
}