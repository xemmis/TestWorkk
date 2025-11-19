using System.Collections;
using UnityEngine;

public class InteractionScannerService : MonoBehaviour, IInteractionScannerService
{
    [SerializeField] private IInteractionRayScanner _interactionRayScanner;
    [SerializeField] private IInteractionVisualFeedBack _interactionVisual;
    [SerializeField] private IPickupable _findedPickable;
    [SerializeField] private IInteractable _findedInteractable;
    [SerializeField] private PickUpService _pickUpService;
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private float _skanTick;

    private void Awake()
    {
        if (_interactionRayScanner != null) StartCoroutine(SkanTick());

        _pickUpService = new PickUpService(_holdPoint);
    }

    public void Initialize(IInteractionRayScanner interactionRayScanner, IInteractionVisualFeedBack interactionVisual)
    {
        _interactionRayScanner = interactionRayScanner;
        _interactionVisual = interactionVisual;

        StartCoroutine(SkanTick());
    }

    private IEnumerator SkanTick()
    {

        MakeSkan();
        yield return new WaitForSeconds(_skanTick);
        StartCoroutine(SkanTick());
    }

    public void MakeSkan()
    {
        _findedPickable = _interactionRayScanner.ScanFor<IPickupable>();
        _findedInteractable = _interactionRayScanner.ScanFor<IInteractable>();

        if (_findedInteractable != null)
        {
            _interactionVisual.ShowInteractionUI();
            print("Found Interactable");
            return;
        }

        if (_findedPickable != null)
        {
            _interactionVisual.ShowPickupUI();
            print("Found Pickupable");
            return;
        }

        _interactionVisual.CleanUI();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pickUpService.PickUpInput(_findedPickable);
        }
    }
}

public class PickUpService
{
    private Transform _holdPoint;
    private IPickupable _currentPickable;

    public PickUpService(Transform HoldPoint)
    {
        _holdPoint = HoldPoint;
    }

    public void PickUpInput(IPickupable pickupable)
    {
        if (_currentPickable != null)
        {
            Drop();
            return;
        }
        _currentPickable = pickupable;

        if (_currentPickable != null)
            PickUp();

    }

    private void PickUp()
    {
        if (_currentPickable.CanPickup) _currentPickable.PickUp(_holdPoint);
    }

    private void Drop()
    {
        _currentPickable.Drop();
        _currentPickable = null;
    }
}