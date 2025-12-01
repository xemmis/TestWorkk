using UnityEngine;

public class PlayerService : MonoBehaviour
{
    public static PlayerService PlayerInstance { get; private set; } = null;
    public Transform GetPlayerPos() => transform;


    private void Awake()
    {
        if (PlayerService.PlayerInstance == null)
        {
            PlayerService.PlayerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
