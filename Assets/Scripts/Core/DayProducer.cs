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
        public Nps PeopleConfigurator;
    }

    [SerializeField] private List<ScheduledSpawnEvent> _schedule = new List<ScheduledSpawnEvent>();
    private Coroutine _dayCoroutine;

    [SerializeField] private float _dayDuration = 60f;
    public UnityEvent OnDayStart;
    public UnityEvent OnDayEnd;

    private void Start()
    {
        StartNewDay();
    }

    public void StartNewDay()
    {
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

        _schedule.Sort((a, b) => a.time.CompareTo(b.time));

        foreach (var scheduledEvent in _schedule)
        {
            yield return new WaitForSeconds(scheduledEvent.time);
            PeopleFabric.PeopleFabricInstance.SpawnPeople(scheduledEvent.PeopleConfigurator);
            Debug.Log($"Event triggered: {scheduledEvent.description} at time {scheduledEvent.time}");
        }

        float elapsedTime = _schedule.Count > 0 ? _schedule[_schedule.Count - 1].time : 0f;
        float remainingTime = _dayDuration - elapsedTime;

        if (remainingTime > 0)
        {
            yield return new WaitForSeconds(remainingTime);
        }

        OnDayEnd?.Invoke();
        Debug.Log("Day ended!");
    }

    public void AddEvent(float time, UnityAction action, Nps peopleConfigurator, string description = "Event")
    {
        var newEvent = new ScheduledSpawnEvent
        {
            PeopleConfigurator = peopleConfigurator,
            time = time,
            description = description,
        };

        _schedule.Add(newEvent);
    }
}
