using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audioSource;
    private float soundEffectVolume;
    private float musicVolume;

    protected override void Awake()
    {
        base.Awake();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start()
    {
        soundEffectVolume = PlayerPrefs.GetFloat(ConstantCollection.soundEffectVolumeString, 1.0f);
        musicVolume = PlayerPrefs.GetFloat(ConstantCollection.musicVolumeString, 1.0f);
    }

    public void ChangeBGM(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public GameObject PlayClip(AudioClip clip)
    {
        GameObject tempObj = new GameObject("SoundSource");
        SoundSource tempComp = tempObj.AddComponent<SoundSource>();
        tempComp.SetClip(clip, soundEffectVolume, ConstantCollection.soundEffectPitchVariance);
        return tempObj;
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
        PlayerPrefs.SetFloat(ConstantCollection.soundEffectVolumeString, volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        PlayerPrefs.SetFloat(ConstantCollection.musicVolumeString, volume);
        PlayerPrefs.Save();
    }
}
