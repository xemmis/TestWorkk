using UnityEngine;
// Базовый класс для обработчиков событий
public abstract class EventHandler : MonoBehaviour
{
    public abstract bool CanHandle(ScheduledEventTheme theme);
    public abstract void Handle(ScheduledEvent scheduledEvent);
}
