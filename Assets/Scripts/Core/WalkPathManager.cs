using System.Collections.Generic;
using UnityEngine;

public class WalkPathManager : MonoBehaviour
{
    [SerializeField] private List<WalkPath> _availablePaths = new List<WalkPath>();
    public static WalkPathManager WalkPathInstance = null;

    private void Awake()
    {
        if (WalkPathManager.WalkPathInstance == null)
        {
            WalkPathManager.WalkPathInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public WalkPath GetRandomPath()
    {
        if (_availablePaths.Count == 0) return null;
        return _availablePaths[Random.Range(0, _availablePaths.Count)];
    }

    public WalkPath GetPathByName(string pathName)
    {
        return _availablePaths.Find(path => path.PathName == pathName);
    }
}
