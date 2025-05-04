using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager Instance;
    public List<Transform> waypointonseen = new List<Transform>();
    public List<Controller> cars = new List<Controller>();

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        cars = LevelManager.Instance.m_CarsOnLevel;
    }
   

    private void Update()
    {
        var carsCopy = new List<Controller>(cars);
        foreach (Controller car in carsCopy)
        {
            if (car != null)
                car.ManagedUpdate();
        }
    }
}
