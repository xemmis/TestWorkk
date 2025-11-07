using UnityEngine;

public interface ISoundService
{
    SoundData Sound { get; }

    void PlaySound(AudioSource audioSource, string SoundName);
}
