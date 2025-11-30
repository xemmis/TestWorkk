using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(WalkPath))]
public class WalkPathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WalkPath walkPath = (WalkPath)target;

        if (GUILayout.Button("Add Current Scene Position"))
        {
            // Можно добавить позицию из сцены
            walkPath.Position = SceneView.lastActiveSceneView.camera.transform.position;
        }
    }
}
#endif