using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Day Events", menuName = "Day Producer")]
public class SceneEvents : ScriptableObject
{
    public List<EventData> eventDatas = new();
}
