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
    [SerializeField] private List<SceneEvents> _daySpawnHandler;
    [SerializeField] private int _currentDay;

    private void Awake()
    {
        _dialogueSystem.Initialize(_dialogueVisualizer, _dialogueInputHandler);
        _interactionScannerService.Initialize(_interactionRayScanner, _interactionVisual);
        NpcConfigurator npsConfigurator = new();
        _peopleFabric.Initialize(_playerPos, npsConfigurator);
        SetDayEvents();
    }

    private void SetDayEvents()
    {
        if (_dayProducer != null)
        {
            _dayProducer.DayEvents = _daySpawnHandler[_currentDay - 1].eventDatas;
        }
    }
}
