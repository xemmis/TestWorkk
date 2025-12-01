using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [Header("Dialogue Components")]
    [SerializeField] DialogueInputHandler _dialogueInputHandler;
    [SerializeField] DialogueUIVisualizer _dialogueVisualizer;
    [SerializeField] DialogueSystem _dialogueSystem;


    [Header("GameCore Components")]
    [SerializeField] private PeopleFabric _peopleFabric;
    [SerializeField] private Transform _playerPos;


    [Header("Interaction Components")]
    [SerializeField] private InteractionScannerService _interactionScannerService;
    [SerializeField] private InteractionRayScanner _interactionRayScanner;
    [SerializeField] private InteractionVisualFeedBack _interactionVisual;

    [Header("DayProducer Components")]
    [SerializeField] private DayProducer _dayProducer;
    [SerializeField] private EventCalendar _eventCalendar;

    [Header("Test")]
    [SerializeField] private string _sceneName;
    [SerializeField] private int _currentDay;

    private void Awake()
    {
        InitializeDialogue();
        InitilizePeopleFabric();
        InitializeEvents();
    }

    private void InitilizePeopleFabric()
    {
        NpcConfigurator npsConfigurator = new();
        _peopleFabric.Initialize(_playerPos, npsConfigurator);
    }

    private void InitializeDialogue()
    {
        _dialogueSystem.Initialize(_dialogueVisualizer, _dialogueInputHandler);
    }

    private void InitializeInteraction()
    {
        _interactionScannerService.Initialize(_interactionRayScanner, _interactionVisual);
    }

    private void InitializeEvents()
    {
        _eventCalendar.SetCurrentDayAndScene(_currentDay, _sceneName);
        if (_dayProducer != null)
        {
            _dayProducer.DayEvents = _eventCalendar.GetDailyEventData();
        }
    }
}
