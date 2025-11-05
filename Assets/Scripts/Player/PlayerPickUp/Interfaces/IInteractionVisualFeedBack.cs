public interface IInteractionVisualFeedBack
{
    InteractionImagesSO InteractionImages { get; }
    void ShowInteractionUI();
    void ShowPickupUI();
    void CleanUI();
}
