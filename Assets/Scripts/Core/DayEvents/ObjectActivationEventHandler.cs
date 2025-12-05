using UnityEngine;

public class ObjectActivationHandler : IEventHandler
{
    public virtual void Execute(EventParameters parameters)
    {
        GameObject targetObject = parameters.objectParam as GameObject;
        if (targetObject == null)
        {
            Debug.LogError("ObjectActivationHandler: No target object specified");
            return;
        }

        // Ищем IActivatable
        IActivatable activatable = targetObject.GetComponent<IActivatable>();
        if (activatable == null)
        {
            Debug.LogError($"Object {targetObject.name} doesn't have IActivatable component");
            return;
        }

        bool shouldActivate = parameters.intParam != 0;

        if (shouldActivate)
        {
            DialogueTree dialogue = parameters.customData as DialogueTree;

            if (dialogue != null)
            {
                activatable.Activate(dialogue);
                Debug.Log($"Activated {targetObject.name} with dialogue: {dialogue.name}");
            }
            else
            {
                activatable.Activate();
                Debug.Log($"Activated {targetObject.name}");
            }
        }
    }
}

public interface IActivatable
{
    void Activate(); // Без параметра для универсальности
    void Activate<T>(T data); // С параметром для конкретных случаев
}

public abstract class BaseActivatable :  IActivatable
{
    public virtual void Activate()
    {
        // Базовая активация без параметров
    }

    public virtual void Activate<T>(T data)
    {
        // Базовая активация с данными
    }

    public virtual void Deactivate()
    {
    }
}
