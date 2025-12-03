#if UNITY_EDITOR
using static UnityEngine.GraphicsBuffer;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(ElectricalPanel))]
public class ElectricalPanelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ElectricalPanel panel = (ElectricalPanel)target;

        if (GUILayout.Button("Find All Connectables in Scene"))
        {
            var connectables = FindObjectsOfType<MonoBehaviour>()
                .OfType<IConnectable>()
                .ToList();

            // Автоматически добавляем в конфиг
        }
    }
}
#endif