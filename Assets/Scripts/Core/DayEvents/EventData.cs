using System;

[System.Serializable]
public class EventData
{
    public EventType EventType;
    public EventParameters Parameters;
    public float TriggerTime;

    public IEventHandler Handler => EventHandlerFactory.Create(EventType);
}

public static class EventHandlerFactory
{
    public static IEventHandler Create(EventType type)
    {
        return type switch
        {
            EventType.NPCSpawn => new SpawnEventHandler(),
            /*
            EventType.SoundPlay => new SoundHandler(),
            EventType.LightToggle => new LightToggleHandler(),
            EventType.ObjectActivation => new ObjectActivationHandler(),
            EventType.DialogueStart => new DialogueStartHandler(),
            */
            _ => throw new ArgumentException($"Unknown event type: {type}")
        };
    }
}