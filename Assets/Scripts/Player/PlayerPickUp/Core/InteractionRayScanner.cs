using UnityEngine;

public class InteractionRayScanner : MonoBehaviour, IInteractionRayScanner
{
    [field: SerializeField] public Camera Camera { get; set; }
    [field: SerializeField] public LayerMask InteractionLayer { get; set; }
    [field: SerializeField] public float Distance { get; set; }

    private void Awake()
    {
        Camera = Camera.main;
    }

    public T ScanFor<T>() where T : class
    {
        Ray ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, Distance, InteractionLayer))
        {
            return hit.collider.GetComponent<T>();
        }
        return null;
    }
}
