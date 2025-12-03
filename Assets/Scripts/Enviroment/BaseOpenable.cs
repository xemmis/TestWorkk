using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class BaseOpenable : MonoBehaviour, IOpenable
{
    [field: SerializeField] public Vector3 OpenPos { get; private protected set; } = Vector3.zero;
    [field: SerializeField] public Vector3 ClosePos { get; private protected set; } = Vector3.zero;
    [field: SerializeField] public bool IsOpened { get; private protected set; } = false;
    [field: SerializeField] public bool CanInteract { get; private protected set; } = true;
    [SerializeField] protected float _duration = 1.5f;
    [SerializeField] protected AudioSource _source = null;
    [Tooltip("Name of Sound in Sound(SO)")]
    [SerializeField] protected string _soundName = "";

    protected virtual void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public virtual void Interact()
    {
        if (IsOpened)
        {
            transform.DOLocalRotate(ClosePos, _duration);
        }
        else
        {
            transform.DOLocalRotate(OpenPos, _duration);
        }

        SoundService.SoundServiceInstance.PlaySound(_soundName, _source);

        IsOpened = !IsOpened;
    }
}
