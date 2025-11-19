using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpsWalkPositions : MonoBehaviour
{
    public List<Transform> PositionsToWalk;   

    public static NpsWalkPositions NpsWalkInstance;

    private void Awake()
    {
        if (NpsWalkPositions.NpsWalkInstance == null) NpsWalkPositions.NpsWalkInstance = this;
        else Destroy(gameObject);
    }
}
