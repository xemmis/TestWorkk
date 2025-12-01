using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    public static GameBootstrap GameBootstrapInstance { get; private set; } = null;

    [Header("Dialogue Components")]
    [SerializeField] DialogueInputHandler _dialogueInputHandler;
    [SerializeField] DialogueUIVisualizer _dialogueVisualizer;
    [SerializeField] DialogueSystem _dialogueSystem;


    [Header("GameCore Components")]
    [SerializeField] private PeopleFabric _peopleFabric;

    [Header("DayProducer Components")]
    [SerializeField] private DayProducer _dayProducer;
    [SerializeField] private EventCalendar _eventCalendar;

    [Header("Test")]
    [SerializeField] private string _sceneName;
    [SerializeField] private int _currentDay;

    private void Awake()
    {
        if (GameBootstrap.GameBootstrapInstance == null)
        {
            GameBootstrap.GameBootstrapInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeDialogue();
        InitilizePeopleFabric();
        InitializeEvents();

        DontDestroyOnLoad(gameObject);
    }

    private void InitilizePeopleFabric()
    {
        NpcConfigurator npsConfigurator = new();
        _peopleFabric.Initialize( npsConfigurator);
    }

    private void InitializeDialogue()
    {
        _dialogueSystem.Initialize(_dialogueVisualizer, _dialogueInputHandler);
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
