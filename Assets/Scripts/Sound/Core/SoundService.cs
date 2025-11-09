using UnityEngine;

public class SoundService : MonoBehaviour, ISoundService
{
    [field: SerializeField] public SoundData Sound { get; private set; }
    public static SoundService SoundServiceInstance;

    private void Awake()
    {
        if (SoundService.SoundServiceInstance == null) SoundService.SoundServiceInstance = this;
        else Destroy(gameObject);
    }

    public void PlaySound(AudioSource audioSource, string SoundName)
    {
        print("pal");
        Sound sound = Sound.GetSound(SoundName);
        audioSource.pitch = sound.Pitch;
        audioSource.volume = sound.Volume;
        audioSource.loop = sound.Loop;
        audioSource.spatialBlend = sound.SpatialBlend;
       
        if (sound.Loop)
        {
            audioSource.clip = sound.AudioClip;
            audioSource.Play();
            return;
        }
        audioSource.PlayOneShot(sound.AudioClip);
    }


    public void StopSound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
