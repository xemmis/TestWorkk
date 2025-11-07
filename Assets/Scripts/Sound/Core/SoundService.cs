using UnityEngine;

public class SoundService : MonoBehaviour, ISoundService
{
    public SoundData Sound { get; private set; }
    public static SoundService SoundServiceInstance;

    private void Awake()
    {
        if (SoundService.SoundServiceInstance == null) SoundService.SoundServiceInstance = this;
        else Destroy(gameObject);
    }

    public void PlaySound(AudioSource audioSource, string SoundName)
    {
        Sound sound = Sound.GetSound(SoundName);
        audioSource.PlayOneShot(sound.AudioClip);
    }
}
