using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [Header("Dialogue Components")]
    [SerializeField] DialogueInputHandler _dialogueInputHandler;
    [SerializeField] DialogueUIVisualizer _dialogueVisualizer;
    [SerializeField] DialogueSystem _dialogueSystem;


    [Header("Cooking Components")]
    [SerializeField] private CoffeeMachine _coffeMachine;
    [SerializeField] private float _coffeeCookTime = 3f;

    [Header("Interaction Components")]
    [SerializeField] private InteractionScannerService _interactionScannerService;
    [SerializeField] private InteractionRayScanner _interactionRayScanner;

    private void Awake()
    {
        _dialogueSystem.Initialize(_dialogueVisualizer, _dialogueInputHandler);
        _coffeMachine.Initialize(new CoffeeCookingService(_coffeeCookTime));
        _interactionScannerService.Initialize(_interactionRayScanner);
    }
}