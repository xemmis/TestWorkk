using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject dialoguePopup;
    [SerializeField] private TextMeshProUGUI npcText;
    [SerializeField] private Transform answersContainer;
    [SerializeField] private GameObject answerButtonPrefab;
    [SerializeField]
    private DialogueNode DialogueNode;
    private DialogueNode _currentNode;
    public static DialogueSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartDialogue(DialogueNode);
    }
    public void StartDialogue(DialogueNode startNode)
    {
        _currentNode = startNode;
        dialoguePopup.SetActive(true);
        ShowCurrentNode();
    }

    public void EndDialogue()
    {
        dialoguePopup.SetActive(false);
        ClearAnswers();
        _currentNode = null;
    }

    private void ShowCurrentNode()
    {
        if (_currentNode == null)
        {
            EndDialogue();
            return;
        }

        npcText.text = _currentNode.npcText;

        ClearAnswers();

        foreach (var answer in _currentNode.answers)
        {
            var buttonObj = Instantiate(answerButtonPrefab, answersContainer);
            var button = buttonObj.GetComponent<AnswerButton>();

            if (button != null)
            {
                button.Setup(answer.text, () => SelectAnswer(answer));
            }
        }
    }

    private void SelectAnswer(DialogueAnswer answer)
    {
        _currentNode = answer.nextNode;
        ShowCurrentNode();
    }

    private void ClearAnswers()
    {
        foreach (Transform child in answersContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
