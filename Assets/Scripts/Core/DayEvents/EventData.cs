using System;
using UnityEngine;

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
            EventType.SoundPlay => new SoundEventHandler(),

            /*
            EventType.LightToggle => new LightToggleHandler(),
            EventType.ObjectActivation => new ObjectActivationHandler(),
            EventType.DialogueStart => new DialogueStartHandler(),
            */
            _ => throw new ArgumentException($"Unknown event type: {type}")
        };
    }
}

public class SoundEventHandler : IEventHandler
{
    public void Execute(EventParameters parameters)
    {
        string SoundName = parameters.stringParam;
        if (SoundName == null)
        {
            return;
        }

        Vector3 vector3 = parameters.vectorParam;

        if (vector3 != Vector3.zero)
        {
            SoundService.SoundServiceInstance.PlaySound(SoundName, vector3);
        }
        else
        {
            SoundService.SoundServiceInstance.PlaySound(SoundName);
        }
    }
}