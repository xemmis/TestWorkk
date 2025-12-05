using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MobilePhone : MonoBehaviour, IInteractable
{
    public static MobilePhone Instance { get; private set; }

    [Header("Visuals")]
    [SerializeField] private string _ringSound = "PhoneRing";
    [SerializeField] private GameObject _phoneModel = null;
    [SerializeField] private AudioSource _audioSource = null;

    [Header("State")]
    private DialogueTree _currentDialogue = null;
    private bool _isRinging = false;

    public bool CanInteract => _isRinging && _currentDialogue != null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Если нужно между сценами

            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Основной метод для активации звонка
    public void StartPhoneCall(DialogueTree dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogError("Cannot start phone call: no dialogue provided");
            return;
        }

        _currentDialogue = dialogue;
        _isRinging = true;

        // Звук
        if (!string.IsNullOrEmpty(_ringSound))
            SoundService.SoundServiceInstance.PlaySound(_ringSound, _audioSource);

        Debug.Log($"Phone ringing with dialogue: {dialogue.name}");
    }

    // Ответ на звонок
    public void AnswerCall()
    {
        if (!_isRinging || _currentDialogue == null)
        {
            Debug.LogWarning("Phone is not ringing or no dialogue");
            return;
        }

        _isRinging = false;

        // Останавливаем звук
        SoundService.SoundServiceInstance.StopSound(_audioSource);

        // Запускаем диалог
        if (DialogueSystem.DialogueSystemInstance != null)
        {
            DialogueSystem.DialogueSystemInstance.StartDialogue(_currentDialogue);
        }
        else
        {
            Debug.LogError("DialogueSystem not found!");
        }

        Debug.Log("Answered phone call");
    }

    // Завершение звонка
    public void EndPhoneCall()
    {
        _isRinging = false;
        _currentDialogue = null;

        if (_phoneModel != null)
            _phoneModel.SetActive(false);

        SoundService.SoundServiceInstance.StopSound(_audioSource);

        Debug.Log("Phone call ended");
    }

    // IInteractable
    public void Interact()
    {
        if (CanInteract)
        {
            AnswerCall();
        }
    }

    // Для внешнего управления
    public bool IsRinging => _isRinging;
    public DialogueTree CurrentDialogue => _currentDialogue;
}
