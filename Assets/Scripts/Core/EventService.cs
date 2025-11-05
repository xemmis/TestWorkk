using System.Collections.Generic;
using UnityEngine;
// Сервис событий с регистрацией обработчиков
public class EventService : MonoBehaviour, IEventService
{
    private List<EventHandler> _eventHandlers = new List<EventHandler>();
    
    private void Awake()
    {
        // Автоматически находим все обработчики на этом GameObject
        _eventHandlers.AddRange(GetComponents<EventHandler>());
    }
    
    public void ExecuteEvent(ScheduledEvent scheduledEvent)
    {
        var handler = _eventHandlers.Find(h => h.CanHandle(scheduledEvent.EventTheme));
        handler?.Handle(scheduledEvent);
    }
}
