using UnityEngine;

[System.Serializable]
public class Sound
{
    public string SoundName;
    public AudioClip AudioClip;
    [Range(0f, 1f)] public float Volume = 1f;
    [Range(0f, 1f)] public float SpatialBlend = 1f;
    [Range(.1f, 3f)] public float Pitch = 1f;
    public bool Loop = true;
}