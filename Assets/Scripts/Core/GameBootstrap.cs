using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [Header("Dialogue Components")]
    [SerializeField] DialogueInputHandler _dialogueInputHandler;
    [SerializeField] DialogueUIVisualizer _dialogueVisualizer;
    [SerializeField] DialogueSystem _dialogueSystem;


    [Header("Cooking Components")]
    [SerializeField] private float _coffeeCookTime = 3f;

    [Header("Interaction Components")]
    [SerializeField] private InteractionScannerService _interactionScannerService;
    [SerializeField] private InteractionRayScanner _interactionRayScanner;
    [SerializeField] private InteractionVisualFeedBack _interactionVisual;

    private void Awake()
    {
        _dialogueSystem.Initialize(_dialogueVisualizer, _dialogueInputHandler);
        _interactionScannerService.Initialize(_interactionRayScanner, _interactionVisual);
    }
}