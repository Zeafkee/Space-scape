using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectionDistance = 0.5f;
    public LayerMask obstacleLayer;

    public int currentIndex = 0;
    public bool isReturning = false;
    public bool isMoving = false;

    [SerializeField] private List<int> pathIndexes; 
    [SerializeField]
    private List<Transform> pathPoints = new List<Transform>();
    [SerializeField]
    private List<Transform> visitedPoints = new List<Transform>();

    private Vector3 startPos;
    public Transform target;

    public void SetPath(List<Transform> points)
    {
        pathPoints = points;
    }

    private void Start()
    {
        startPos = transform.position;

        foreach (int index in pathIndexes)
        {
            if (index >= 0 && index < ShipManager.Instance.waypointonseen.Count)
            {
                pathPoints.Add(ShipManager.Instance.waypointonseen[index]);
            }

            LevelManager.Instance.AddCar(this.GetComponent<Controller>());
        }

       // StartMovement();
    }

    public void StartMovement()
    {
        if (pathPoints.Count == 0) return;
        isMoving = true;
        currentIndex = 0;
        visitedPoints.Clear();
    }

    public void ManagedUpdate()
    {
        if (!isMoving) return;

        if (ObstacleAhead())
        {
            if (!isReturning)
                StartReturn();
        }

        Move();
    }

    void Move()
    {
        Debug.Log("zzzzzzzzzzz");
        if (!isReturning)
        {
            if (currentIndex >= pathPoints.Count)
            {
                isMoving = false;
                return;
            }

            target = pathPoints[currentIndex];
            Debug.Log("hhhhhhhhhhhh");
        }
        else
        {
            if (currentIndex >= visitedPoints.Count)
            {
                StopReturn();
                return;
            }
            target = visitedPoints[currentIndex];
            Debug.Log("hahaha");
        }

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.forward = dir;

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            if (!isReturning)
                visitedPoints.Add(pathPoints[currentIndex]);

            currentIndex++;
        }
        if (currentIndex == pathPoints.Count)
            LevelManager.Instance.DeleteCar(this.gameObject);
    }
    bool ObstacleAhead()
    {
        return Physics.Raycast(transform.position, transform.forward, detectionDistance, obstacleLayer);
    }

    void StartReturn()
    {
        isReturning = true;
        visitedPoints.Reverse();
        visitedPoints.Add(CreateTempPoint(startPos));
        currentIndex = 0;
    }

    void StopReturn()
    {
        isMoving = false;
        isReturning = false;
        currentIndex = 0;
        visitedPoints.Clear();
    }

    Transform CreateTempPoint(Vector3 position)
    {
        GameObject temp = new GameObject("StartPos");
        temp.transform.position = position;
        return temp.transform;
    }
}
