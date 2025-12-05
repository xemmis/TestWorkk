using UnityEngine;

public class MobileActivator : MonoBehaviour, IActivatable
{
    [Header("Settings")]
    [SerializeField] private DialogueTree _defaultDialogue;

    public void Activate()
    {
        // Активация с дефолтным диалогом
        if (_defaultDialogue != null)
        {
            Activate(_defaultDialogue);
        }
        else
        {
            Debug.LogError("MobileActivator: No default dialogue set!");
        }
    }

    public void Activate<T>(T data)
    {
        if (data is DialogueTree dialogue)
        {
            StartPhoneCall(dialogue);
        }
        else
        {
            Debug.LogError($"MobileActivator: Invalid data type {typeof(T)}");
        }
    }

    private void StartPhoneCall(DialogueTree dialogue)
    {
        if (MobilePhone.Instance == null)
        {
            Debug.LogError("MobilePhone instance not found!");
            return;
        }

        MobilePhone.Instance.StartPhoneCall(dialogue);
        Debug.Log($"MobileActivator started phone call with: {dialogue.name}");
    }

    // Для удобства в инспекторе
    private void OnValidate()
    {
        if (_defaultDialogue == null)
        {
            Debug.LogWarning("MobileActivator: Consider setting a default dialogue");
        }
    }
}