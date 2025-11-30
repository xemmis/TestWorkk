using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkPath_", menuName = "NPC/Walk Path", order = 4)]
public class WalkPath : ScriptableObject
{
    public string PathName;
    public Vector3 Position = Vector3.zero;

    [Header("Gizmos")]
    public Color PathColor = Color.cyan;
    public float PointSize = 0.3f;
}
