using UnityEngine;

public interface IInteractionRayScanner
{
    Camera Camera { get; set; }
    LayerMask InteractionLayer { get; set; }
    float Distance { get; set; }
    T ScanFor<T>() where T : class;
}
