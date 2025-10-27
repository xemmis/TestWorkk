using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueUIVisualizer : MonoBehaviour, IDialogueVisualizer
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    public void CleanUpUI()
    {
        if (_textMeshPro != null) _textMeshPro.text = "";
    }

    public void Visualize(DialogueNode dialogueNode)
    {
        if (dialogueNode.npcText == null) return;
        
        _textMeshPro.text = dialogueNode.npcText;
        _textMeshPro.maxVisibleCharacters = 0;
        StartCoroutine(RevealCharacters());
    }

    private IEnumerator RevealCharacters()
    {
        int totalCharacters = _textMeshPro.text.Length;
        int counter = 0;

        while (counter < totalCharacters)
        {
            counter++;
            _textMeshPro.maxVisibleCharacters = counter;
            yield return new WaitForSeconds(0.02f);
        }
    }
}