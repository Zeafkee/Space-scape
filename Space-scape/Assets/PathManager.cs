using UnityEngine;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{
    public List<Transform> pathPoints = new List<Transform>();
    public static PathManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        pathPoints.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            pathPoints.Add(transform.GetChild(i));
        }
    }
}
