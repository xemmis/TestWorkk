using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundService : MonoBehaviour, ISoundService
{
    [field: SerializeField] public SoundData Sound { get; private set; }
    private AudioSource _audioSource = null;
    public static SoundService SoundServiceInstance;

    private void Awake()
    {
        if (SoundServiceInstance == null)
        {
            SoundServiceInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName, AudioSource audioSource = null)
    {
        audioSource = audioSource ?? _audioSource;

        Sound sound = Sound.GetSound(soundName);
        if (sound == null)
        {
            Debug.LogWarning($"Sound '{soundName}' not found!");
            return;
        }

        ConfigureAudioSource(audioSource, sound);

        if (sound.Loop)
        {
            audioSource.clip = sound.AudioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(sound.AudioClip);
        }
    }

    public void PlaySound(string soundName, Vector3 position, AudioSource audioSource = null)
    {
        transform.position = position;
        PlaySound(soundName, audioSource);
    }


    public void StopSound(AudioSource audioSource = null)
    {
        transform.position = Vector3.zero;

        audioSource = audioSource ?? _audioSource;
        audioSource?.Stop();
    }

    private void ConfigureAudioSource(AudioSource audioSource, Sound sound)
    {
        audioSource.pitch = sound.Pitch;
        audioSource.volume = sound.Volume;
        audioSource.loop = sound.Loop;
        audioSource.spatialBlend = sound.SpatialBlend;
    }
}
