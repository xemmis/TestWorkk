using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayProducer : MonoBehaviour, IDayProducer
{
    public Action<IEventHandler> OnEventTriggered { get; private set; } = null;
    [SerializeField] private List<EventData> _dayEvents = new();
    private Coroutine _schedulerCoroutine = null;
    private bool _isRunning = false;

    public List<EventData> DayEvents
    {
        get => _dayEvents;
        set => _dayEvents = value ?? new List<EventData>();
    }

    public void AddEvent(EventData eventData)
    {
        _dayEvents.Add(eventData);
        _dayEvents = _dayEvents.OrderBy(e => e.TriggerTime).ToList();
    }

    private void Start()
    {
        StartScheduler();
    }

    public void StartScheduler()
    {
        if (_isRunning) return;

        _isRunning = true;
        _schedulerCoroutine = StartCoroutine(EventScheduler());
    }

    public void StopScheduler()
    {
        _isRunning = false;
        if (_schedulerCoroutine != null)
        {
            StopCoroutine(_schedulerCoroutine);
            _schedulerCoroutine = null;
        }
    }

    private IEnumerator EventScheduler()
    {
        float dayTimer = 0f;
        int nextEventIndex = 0;

        var sortedEvents = _dayEvents.OrderBy(e => e.TriggerTime).ToList();

        while (_isRunning && nextEventIndex < sortedEvents.Count)
        {
            dayTimer += Time.deltaTime;

            if (nextEventIndex < sortedEvents.Count &&
                dayTimer >= sortedEvents[nextEventIndex].TriggerTime)
            {
                EventData currentEvent = sortedEvents[nextEventIndex];

                currentEvent.Handler?.Execute(currentEvent.Parameters);

                OnEventTriggered?.Invoke(currentEvent.Handler);

                nextEventIndex++;
            }

            yield return null;
        }

        _isRunning = false;
    }

    void OnDestroy()
    {
        StopScheduler();
    }
}