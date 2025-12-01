using UnityEngine;

public class BedLogic : MonoBehaviour, IInteractable
{
    public bool CanInteract { get; private set; } = true;

    public void Interact()
    {
        if (!CanInteract)
        {
            return;
        }

        SceneSwitchManager.SwitchInstance.NextSceneForCurrentDay();
    }
}