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
}
