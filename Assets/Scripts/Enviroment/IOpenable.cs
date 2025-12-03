using UnityEngine;

public interface IOpenable : IInteractable
{
    public Vector3 OpenPos { get; }
    public Vector3 ClosePos { get; }
    public bool IsOpened { get; }
}
