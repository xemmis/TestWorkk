using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventCalendar))]
public class EventCalendarEditor : Editor
{
    private SerializedProperty _eventsProperty;
    private SerializedProperty _currentDayProperty;

    private void OnEnable()
    {
        _eventsProperty = serializedObject.FindProperty("Events");
        _currentDayProperty = serializedObject.FindProperty("CurrentDay");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Текущий день
        EditorGUILayout.PropertyField(_currentDayProperty);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Расписание событий", EditorStyles.boldLabel);

        // Отображаем события
        if (_eventsProperty.arraySize > 0)
        {
            DrawEventsTable();
        }
        else
        {
            EditorGUILayout.HelpBox("Нет событий", MessageType.Info);
        }

        // Кнопка добавления
        EditorGUILayout.Space();
        if (GUILayout.Button("Добавить день"))
        {
            _eventsProperty.arraySize++;
            var newEntry = _eventsProperty.GetArrayElementAtIndex(_eventsProperty.arraySize - 1);
            newEntry.FindPropertyRelative("Day").intValue = _eventsProperty.arraySize;
            newEntry.FindPropertyRelative("SceneName").stringValue = "Scene_1";
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawEventsTable()
    {
        // Заголовок таблицы
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("День", EditorStyles.boldLabel, GUILayout.Width(60));
                EditorGUILayout.LabelField("Сцена", EditorStyles.boldLabel, GUILayout.Width(150));
                EditorGUILayout.LabelField("События", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("", GUILayout.Width(30)); // Для кнопки удаления
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            // Строки с днями
            for (int i = 0; i < _eventsProperty.arraySize; i++)
            {
                DrawDayEntry(i);
            }
        }
        EditorGUILayout.EndVertical();
    }

    private void DrawDayEntry(int index)
    {
        var dayEntry = _eventsProperty.GetArrayElementAtIndex(index);
        var dayProperty = dayEntry.FindPropertyRelative("Day");
        var sceneProperty = dayEntry.FindPropertyRelative("SceneName");
        var eventsProperty = dayEntry.FindPropertyRelative("Events");

        EditorGUILayout.BeginHorizontal();
        {
            // День
            EditorGUILayout.PropertyField(dayProperty, GUIContent.none, GUILayout.Width(60));

            // Название сцены
            EditorGUILayout.PropertyField(sceneProperty, GUIContent.none, GUILayout.Width(150));

            // Количество событий + кнопка развернуть
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField($"Событий: {eventsProperty.arraySize}", GUILayout.MinWidth(100));

                // Кнопка для редактирования событий
                if (GUILayout.Button("Изменить", GUILayout.Width(70)))
                {
                    eventsProperty.isExpanded = !eventsProperty.isExpanded;
                }
            }
            EditorGUILayout.EndVertical();

            // Кнопка удаления дня
            if (GUILayout.Button("×", GUILayout.Width(25)))
            {
                _eventsProperty.DeleteArrayElementAtIndex(index);
                return;
            }
        }
        EditorGUILayout.EndHorizontal();

        // Развернутое отображение событий
        if (eventsProperty.isExpanded)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(eventsProperty, true);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space(5);
    }
}