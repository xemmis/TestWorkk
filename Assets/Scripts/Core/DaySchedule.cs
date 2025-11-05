using System.Collections.Generic;
using UnityEngine;
// Дополнительно: Конфигурация через ScriptableObject для переиспользования
[CreateAssetMenu(menuName = "Game/Day Schedule")]
public class DaySchedule : ScriptableObject
{
    public List<ScheduledEvent> scheduleEvents = new List<ScheduledEvent>();
    public float dayDuration = 60f;
}
