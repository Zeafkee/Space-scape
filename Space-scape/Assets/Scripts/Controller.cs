using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectionDistance = 0.5f;
    public LayerMask obstacleLayer;

    private int currentIndex = 0;
    private bool isReturning = false;
    private bool isMoving = false;
    [SerializeField]
    private List<Transform> pathPoints;
    [SerializeField]
    private List<Transform> visitedPoints = new List<Transform>();
    private Vector3 startPos;
    public PathManager pathManager;

    private void Start()
    {
        PathManager pathManager = GetComponentInParent<PathManager>();
        if (pathManager != null)
        {
            pathPoints = pathManager.pathPoints;
        }
        startPos = transform.position;
    }

    private void Update()
    {
        if (!isMoving) return;

        if (ObstacleAhead())
        {
            if (!isReturning)
            {
                StartReturn();
            }
        }

        Move();
    }

    public void StartMovement()
    {
        if (pathPoints.Count == 0) return;
        isMoving = true;
        currentIndex = 0;
        visitedPoints.Clear();
    }

    void Move()
    {
        Transform target;

        if (!isReturning)
        {
            if (currentIndex >= pathPoints.Count)
            {
                Destroy(gameObject);
                return;
            }

            target = pathPoints[currentIndex];
        }
        else
        {
            if (currentIndex >= visitedPoints.Count)
            {
                StopReturn();
                return;
            }

            target = visitedPoints[currentIndex];
        }

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.forward = dir;

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            if (!isReturning)
                visitedPoints.Add(pathPoints[currentIndex]);

            currentIndex++;
        }
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
