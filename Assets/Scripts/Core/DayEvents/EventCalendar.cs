using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventCalendar", menuName = "NPC/Calendar")]
public class EventCalendar : ScriptableObject
{
    [System.Serializable]
    public class DayEvents
    {
        public string SceneName;
        public List<EventData> Events;
        public int Day;
    }

    public List<DayEvents> Events = new();
    public int CurrentDay = 1;
    public string CurrentScene = "";

    public List<EventData> GetCurrentEvents()
    {
        return GetEventsForDayAndScene(CurrentDay, CurrentScene);
    }

    public List<EventData> GetEventsForDayAndScene(int day, string sceneName)
    {
        var dayEvents = GetEventsForDay(day);

        if (!string.IsNullOrEmpty(sceneName))
        {
            return dayEvents?.Find(e => e.SceneName == sceneName)?.Events ?? new List<EventData>();
        }

        return GetAllEventsForDay(day);
    }

    public List<DayEvents> GetEventsForDay(int day)
    {
        return Events.FindAll(e => e.Day == day);
    }

    public List<EventData> GetAllEventsForDay(int day)
    {
        var result = new List<EventData>();
        var dayEventsList = GetEventsForDay(day);

        foreach (var dayEvents in dayEventsList)
        {
            if (dayEvents.Events != null)
            {
                result.AddRange(dayEvents.Events);
            }
        }

        return result;
    }

    // �������� ������� ��� ������� ����� (���� ����)
    public List<EventData> GetEventsForCurrentScene()
    {
        return GetEventsForScene(CurrentScene);
    }

    // �������� ������� ��� ���������� ����� (���� ����)
    public List<EventData> GetEventsForScene(string sceneName)
    {
        var result = new List<EventData>();

        foreach (var dayEvents in Events)
        {
            if (dayEvents.SceneName == sceneName && dayEvents.Events != null)
            {
                result.AddRange(dayEvents.Events);
            }
        }

        return result;
    }

    // �������� ��� ���������� ����� ���� ��� ����������� ���
    public List<string> GetScenesForDay(int day)
    {
        var scenes = new List<string>();
        var dayEventsList = GetEventsForDay(day);

        foreach (var dayEvents in dayEventsList)
        {
            if (!string.IsNullOrEmpty(dayEvents.SceneName) && !scenes.Contains(dayEvents.SceneName))
            {
                scenes.Add(dayEvents.SceneName);
            }
        }

        return scenes;
    }

    // ���������, ���� �� ������� ��� ������� ����������
    public bool HasCurrentEvents()
    {
        return GetCurrentEvents().Count > 0;
    }

    // ���������, ���� �� ������� ��� ��� � �����
    public bool HasEventsForDayAndScene(int day, string sceneName)
    {
        return GetEventsForDayAndScene(day, sceneName).Count > 0;
    }

    // ���������� ���� � ����� ������������
    public void SetCurrentDayAndScene(int day, string sceneName)
    {
        SetDay(day);
        SetScene(sceneName);
    }

    // ���������� �����
    public void SetScene(string sceneName)
    {
        CurrentScene = sceneName;
    }

    // �������� DayEvents �� ��� � �����
    public DayEvents GetDayEvents(int day, string sceneName)
    {
        return Events.Find(e => e.Day == day && e.SceneName == sceneName);
    }

    // �������� ������� ��� �������� ��� � �����
    public void AddEventToCurrent(EventData eventData)
    {
        AddEventToDayAndScene(CurrentDay, CurrentScene, eventData);
    }

    // �������� ������� ��� ����������� ��� � �����
    public void AddEventToDayAndScene(int day, string sceneName, EventData eventData)
    {
        var dayEvents = GetDayEvents(day, sceneName);

        if (dayEvents == null)
        {
            // ������� ����� ������ ���� �� ����������
            dayEvents = new DayEvents
            {
                Day = day,
                SceneName = sceneName,
                Events = new List<EventData>()
            };
            Events.Add(dayEvents);
        }

        dayEvents.Events.Add(eventData);
    }

    // ������ ������ ��� �������� �������������
    public DayEvents GetDailyEvents()
    {
        return GetDayEvents(CurrentDay, CurrentScene);
    }

    public List<EventData> GetDailyEventData()
    {
        return GetCurrentEvents();
    }

    public void SetDay(int day)
    {
        CurrentDay = day;
    }
}