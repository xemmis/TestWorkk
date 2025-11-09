using UnityEngine;
// Обработчик спавна NPC
public class SpawnEventHandler : EventHandler
{
    [SerializeField] private PeopleFabric _npsFabric;

    public override bool CanHandle(ScheduledEventTheme theme)
        => theme == ScheduledEventTheme.Spawn;

    public override void Handle(ScheduledEvent scheduledEvent)
    {
        if (scheduledEvent.Nps != null)
            _npsFabric.SpawnNps(scheduledEvent.Nps);
    }
}
