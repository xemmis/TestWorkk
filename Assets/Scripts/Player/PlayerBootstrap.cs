
using UnityEngine;

public class PlayerBootstrap : MonoBehaviour
{
    [Header("Interaction Components")]
    [SerializeField] private InteractionScannerService _interactionScannerService;
    [SerializeField] private InteractionRayScanner _interactionRayScanner;
    [SerializeField] private InteractionVisualFeedBack _interactionVisual;

    private void Awake()
    {
        _interactionScannerService.Initialize(_interactionRayScanner, _interactionVisual);
    }
}
