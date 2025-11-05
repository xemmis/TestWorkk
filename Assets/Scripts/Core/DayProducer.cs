using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Основной класс - исправленный
public class DayProducer : MonoBehaviour, IDayProducer
{
    [SerializeField] private List<ScheduledEvent> _scheduleEvents = new List<ScheduledEvent>();
    [SerializeField] private float _dayDuration = 60f;
    [SerializeField] private EventService _eventService;
    
    private Coroutine _dayCoroutine;
    private List<ScheduledEvent> _sortedEvents;
    
    public UnityEvent OnDayStart { get; } = new UnityEvent();
    public UnityEvent OnDayEnd { get; } = new UnityEvent();
    
    private void Awake()
    {
        // Сортируем события один раз при инициализации
        _sortedEvents = new List<ScheduledEvent>(_scheduleEvents);
        _sortedEvents.Sort((a, b) => a.time.CompareTo(b.time));
    }
    
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
        
        float lastEventTime = 0f;
        
        foreach (var scheduledEvent in _sortedEvents)
        {
            // Ждем разницу между текущим и предыдущим событием
            float waitTime = scheduledEvent.time - lastEventTime;
            if (waitTime > 0)
            {
                yield return new WaitForSeconds(waitTime);
            }
            
            _eventService.ExecuteEvent(scheduledEvent);
            Debug.Log($"Event triggered: {scheduledEvent.description} at time {scheduledEvent.time}");
            
            lastEventTime = scheduledEvent.time;
        }
        
        // Ждем оставшееся время дня
        float remainingTime = _dayDuration - lastEventTime;
        if (remainingTime > 0)
        {
            yield return new WaitForSeconds(remainingTime);
        }
        
        OnDayEnd?.Invoke();
        Debug.Log("Day ended!");
    }
}
