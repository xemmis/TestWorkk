using UnityEngine;

public interface ISoundService
{
    SoundData Sound { get; }

    void PlaySound(string soundName, AudioSource audioSource = null);
    void StopSound(AudioSource audioSource);
}
