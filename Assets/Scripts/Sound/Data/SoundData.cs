using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio", menuName = "SoundData", order = 0)]
public class SoundData : ScriptableObject
{
    public List<Sound> Sounds = new List<Sound>();

    public Sound GetSound(string soundName)
    {
        return Sounds.Find(s => s.SoundName == soundName);
    }
}
