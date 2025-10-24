using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayProducer : MonoBehaviour, IDayProducer
{
    [System.Serializable]
    public class ScheduledSpawnEvent
    {
        public string description;
        public float time;
        public PeopleConfigurator PeopleConfigurator;
    }
    [System.Serializable]
    public class TestEvent
    {
        public string description;
        public float time;
    }

    [SerializeField] private List<ScheduledSpawnEvent> _schedule = new List<ScheduledSpawnEvent>();
    private Coroutine _dayCoroutine;

    [SerializeField] private float _dayDuration = 60f; // Длительность дня в секундах
    public UnityEvent OnDayStart;
    public UnityEvent OnDayEnd;

    private void Start()
    {
        StartNewDay();
    }

    public void StartNewDay()
    {
        // Останавливаем предыдущий день если запущен
        if (_dayCoroutine != null)
        {
            StopCoroutine(_dayCoroutine);
        }

        _dayCoroutine = StartCoroutine(DayRoutine());
    }

    private System.Collections.IEnumerator DayRoutine()
    {
        OnDayStart?.Invoke();
        Debug.Log("Day started!");

        // Сортируем события по времени
        _schedule.Sort((a, b) => a.time.CompareTo(b.time));

        foreach (var scheduledEvent in _schedule)
        {
            // Ждем пока не наступит время события
            yield return new WaitForSeconds(scheduledEvent.time);
            PeopleFabric.FabricInstance.SpawnPeople(scheduledEvent.PeopleConfigurator, transform);
            Debug.Log($"Event triggered: {scheduledEvent.description} at time {scheduledEvent.time}");
        }

        // Ждем окончания дня
        float elapsedTime = _schedule.Count > 0 ? _schedule[_schedule.Count - 1].time : 0f;
        float remainingTime = _dayDuration - elapsedTime;

        if (remainingTime > 0)
        {
            yield return new WaitForSeconds(remainingTime);
        }

        OnDayEnd?.Invoke();
        Debug.Log("Day ended!");
    }

    public void AddEvent(float time, UnityAction action, PeopleConfigurator peopleConfigurator, string description = "Event")
    {
        var newEvent = new ScheduledSpawnEvent
        {            
            PeopleConfigurator = peopleConfigurator,
            time = time,
            description = description,
        };

        _schedule.Add(newEvent);
    }

    // Пример использования из инспектора
    [ContextMenu("Add Test Event")]
    public void AddTestEvent()
    {
        AddEvent(5f, () => Debug.Log("Test event triggered!"),null, "Test Event");
    }

}
