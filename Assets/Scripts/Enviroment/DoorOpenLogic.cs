using DG.Tweening;
using UnityEngine;

public class DoorOpenLogic : MonoBehaviour, IInteractable
{
    public bool CanInteract { get; private set; } = true;
    [SerializeField] private Vector3 _doorOpenPos;
    [SerializeField] private Vector3 _doorClosePos;
    [SerializeField] private bool _isOpened;

    public void Interact()
    {
        if (_isOpened)
        {
            transform.DORotate(_doorClosePos, 1.5f);
        }
        else
        {
            transform.DORotate(_doorOpenPos, 1.5f);
        }

        _isOpened = !_isOpened;
    }
}
