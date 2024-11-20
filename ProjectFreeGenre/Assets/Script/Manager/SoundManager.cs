using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audioSource;
    public float musicVolume { get; private set; }
    public float soundEffectVolume { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start()
    {
        musicVolume = PlayerPrefs.GetFloat(ConstantCollection.musicVolumeString, 1.0f);
        soundEffectVolume = PlayerPrefs.GetFloat(ConstantCollection.soundEffectVolumeString, 1.0f);
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

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(ConstantCollection.musicVolumeString, volume);
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
        PlayerPrefs.SetFloat(ConstantCollection.soundEffectVolumeString, volume);
    }
}
