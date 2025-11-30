using System.Collections.Generic;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    [field: SerializeField] public static DayCounter DayCounterInstance { get; private set; }

    [field: SerializeField] public Dictionary<int, List<EventData>> DayEvents { get; private set; }

    private void Awake()
    {
        if (DayCounter.DayCounterInstance == null)
        {
            DayCounter.DayCounterInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
