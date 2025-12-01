using UnityEditor;
using UnityEngine;

public static class EditorInputDialog
{
    public delegate void OnInputDialogResult(string result);

    private static string _inputValue = "";
    private static OnInputDialogResult _callback;

    public static void Show(string title, string message, string defaultValue, OnInputDialogResult callback)
    {
        _inputValue = defaultValue;
        _callback = callback;

        var window = EditorWindow.GetWindow<InputDialogWindow>(true, title);
        window.Show();
    }

    private class InputDialogWindow : EditorWindow
    {
        private void OnEnable()
        {
            minSize = new Vector2(300, 120);
            maxSize = new Vector2(300, 120);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Диалог ввода", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            _inputValue = EditorGUILayout.TextField("Введите значение:", _inputValue);
            EditorGUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Отмена"))
                {
                    Close();
                }

                if (GUILayout.Button("OK"))
                {
                    _callback?.Invoke(_inputValue);
                    Close();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}