using System.Collections.Generic;
using System;
using System.Linq;

public static class EventBus
{
    private static readonly Dictionary<Type, List<Action<object>>> _eventHandlers = new();

    public static void Subscribe<T>(Action<T> handler) where T : class
    {
        var eventType = typeof(T);

        if (!_eventHandlers.ContainsKey(eventType))
            _eventHandlers[eventType] = new List<Action<object>>();

        _eventHandlers[eventType].Add(obj => handler(obj as T));
    }

    public static void Unsubscribe<T>(Action<T> handler) where T : class
    {
        var eventType = typeof(T);

        if (_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType].RemoveAll(h =>
                h.Target == handler.Target && h.Method == handler.Method);
        }
    }

    public static void Publish<T>(T eventData) where T : class
    {
        var eventType = typeof(T);

        if (_eventHandlers.ContainsKey(eventType))
        {
            foreach (var handler in _eventHandlers[eventType].ToList())
            {
                handler?.Invoke(eventData);
            }
        }
    }

    public static void Clear() => _eventHandlers.Clear();
}
