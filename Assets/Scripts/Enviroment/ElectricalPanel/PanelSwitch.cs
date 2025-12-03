using UnityEngine.Events;

public class PanelSwitch : BaseOpenable
{
    public UnityEvent OnSwitchToggled { get; private set; } = new();

    public override void Interact()
    {
        base.Interact();

        OnSwitchToggled?.Invoke();
    }
}
