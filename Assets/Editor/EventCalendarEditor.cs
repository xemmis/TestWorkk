using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventCalendar))]
public class EventCalendarEditor : Editor
{
    private SerializedProperty _eventsProperty;
    private SerializedProperty _currentDayProperty;
    private SerializedProperty _currentSceneProperty; // Новое свойство

    private void OnEnable()
    {
        _eventsProperty = serializedObject.FindProperty("Events");
        _currentDayProperty = serializedObject.FindProperty("CurrentDay");
        _currentSceneProperty = serializedObject.FindProperty("CurrentScene"); // Получаем ссылку
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Текущее состояние", EditorStyles.boldLabel);
        
        // Текущий день и сцена в одной строке
        EditorGUILayout.BeginHorizontal();
        {
            // День
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("День", EditorStyles.miniLabel);
                EditorGUILayout.PropertyField(_currentDayProperty, GUIContent.none);
            }
            EditorGUILayout.EndVertical();

            // Сцена
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("Сцена", EditorStyles.miniLabel);
                EditorGUILayout.PropertyField(_currentSceneProperty, GUIContent.none);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

        // Информация о текущих событиях
        DrawCurrentStateInfo();

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

        // Кнопки управления
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Добавить день"))
            {
                AddNewDay();
            }

            if (GUILayout.Button("Сортировать по дням"))
            {
                SortByDays();
            }
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCurrentStateInfo()
    {
        EventCalendar calendar = (EventCalendar)target;
        
        int day = _currentDayProperty.intValue;
        string scene = _currentSceneProperty.stringValue;
        
        var eventsForDay = calendar.GetEventsForDay(day);
        int totalEvents = 0;
        
        if (eventsForDay != null)
        {
            foreach (var dayEvent in eventsForDay)
            {
                if (dayEvent.Events != null)
                    totalEvents += dayEvent.Events.Count;
            }
        }

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        {
            EditorGUILayout.LabelField($"Событий на день {day}: {totalEvents}", EditorStyles.miniLabel);
            
            if (!string.IsNullOrEmpty(scene))
            {
                var sceneEvents = calendar.GetEventsForDayAndScene(day, scene);
                EditorGUILayout.LabelField($"Событий в сцене '{scene}': {sceneEvents?.Count ?? 0}", EditorStyles.miniLabel);
            }
        }
        EditorGUILayout.EndVertical();
    }

    private void DrawEventsTable()
    {
        // Заголовок таблицы
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("День", EditorStyles.boldLabel, GUILayout.Width(50));
                EditorGUILayout.LabelField("Сцена", EditorStyles.boldLabel, GUILayout.Width(150));
                EditorGUILayout.LabelField("События", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("", GUILayout.Width(60)); // Для кнопок
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

        // Подсветка текущего дня и сцены
        bool isCurrentDay = dayProperty.intValue == _currentDayProperty.intValue;
        bool isCurrentScene = sceneProperty.stringValue == _currentSceneProperty.stringValue;
        
        if (isCurrentDay && isCurrentScene)
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.textArea);
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
        }

        // День
        EditorGUILayout.BeginVertical(GUILayout.Width(50));
        {
            if (isCurrentDay)
            {
                GUI.color = Color.green;
            }
            EditorGUILayout.PropertyField(dayProperty, GUIContent.none);
            GUI.color = Color.white;
        }
        EditorGUILayout.EndVertical();

        // Название сцены
        EditorGUILayout.BeginVertical(GUILayout.Width(150));
        {
            if (isCurrentScene)
            {
                GUI.color = Color.yellow;
            }
            EditorGUILayout.PropertyField(sceneProperty, GUIContent.none);
            GUI.color = Color.white;
        }
        EditorGUILayout.EndVertical();

        // Количество событий
        EditorGUILayout.BeginVertical();
        {
            string eventCountText = $"{eventsProperty.arraySize} событий";
            
            if (eventsProperty.arraySize == 0)
            {
                GUI.color = Color.red;
                eventCountText = "Нет событий";
            }
            
            EditorGUILayout.LabelField(eventCountText, GUILayout.MinWidth(100));
            GUI.color = Color.white;
        }
        EditorGUILayout.EndVertical();

        // Кнопки управления
        EditorGUILayout.BeginVertical(GUILayout.Width(60));
        {
            EditorGUILayout.BeginHorizontal();
            {
                // Кнопка редактирования
                if (GUILayout.Button("✎", GUILayout.Width(25)))
                {
                    eventsProperty.isExpanded = !eventsProperty.isExpanded;
                }

                // Кнопка удаления
                if (GUILayout.Button("×", GUILayout.Width(25)))
                {
                    _eventsProperty.DeleteArrayElementAtIndex(index);
                    return;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

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

    private void AddNewDay()
    {
        int newDay = 1;
        if (_eventsProperty.arraySize > 0)
        {
            // Находим максимальный день
            for (int i = 0; i < _eventsProperty.arraySize; i++)
            {
                var dayEntry = _eventsProperty.GetArrayElementAtIndex(i);
                var dayProperty = dayEntry.FindPropertyRelative("Day");
                if (dayProperty.intValue >= newDay)
                {
                    newDay = dayProperty.intValue + 1;
                }
            }
        }

        _eventsProperty.arraySize++;
        var newEntry = _eventsProperty.GetArrayElementAtIndex(_eventsProperty.arraySize - 1);
        newEntry.FindPropertyRelative("Day").intValue = newDay;
        newEntry.FindPropertyRelative("SceneName").stringValue = "Scene_1";
        newEntry.FindPropertyRelative("Events").arraySize = 0;
    }

    private void SortByDays()
    {
        // Сортировка по дням
        // (Нужно реализовать логику сортировки массива)
        Debug.Log("Сортировка по дням");
    }
}