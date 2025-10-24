using TMPro;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answerText;
    private System.Action _onClick;

    public void Setup(string text, System.Action onClick)
    {
        answerText.text = text;
        _onClick = onClick;
    }

    public void OnClick()
    {
        _onClick?.Invoke();
    }
}