using UnityEngine;
using UnityEngine.UI;

public class InteractionVisualFeedBack : MonoBehaviour, IInteractionVisualFeedBack
{
    [SerializeField] private Image _interactionImage;
    [field: SerializeField] public InteractionImagesSO InteractionImages { get; private set; }

    private void Start()
    {
        if (_interactionImage == null) _interactionImage = GetComponentInChildren<Image>();
    }

    public void ShowInteractionUI()
    {
        _interactionImage.sprite = InteractionImages.InteractionSprite;

    }

    public void ShowPickupUI()
    {
        _interactionImage.sprite = InteractionImages.PickupSprite;
    }

    public void CleanUI()
    {
        _interactionImage.sprite = InteractionImages.RegularSprite;
    }
}
