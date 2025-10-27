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

    private void Awake()
    {
        _dialogueSystem.Initialize(_dialogueVisualizer, _dialogueInputHandler);
        _coffeMachine.Initialize(new CoffeeCookingService(_coffeeCookTime));
    }
}