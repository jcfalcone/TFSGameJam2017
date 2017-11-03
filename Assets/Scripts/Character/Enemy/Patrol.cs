using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private int pathNodes;
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float minZ;
    [SerializeField]
    private float maxZ;
    [SerializeField]
    private float pathNodeProximityTolerance;
    private enum PathType { Sequential, Straight, Short/*, StraightAndShort*/ };
    [SerializeField]
    private PathType pathType;


    public struct PathNode
    {
        private Vector3 _position;

        bool _visited;

        public Vector3 position
        {
            get { return _position; }
            set { _position = value; }
        }

        public bool visited
        {
            get { return _visited; }
            set { _visited = value; }
        }
    }

    int _currentPathNode;
    [SerializeField]
    private PathNode[] path;

    // public Transform marker;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void PatrolAround()
    {
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }

        if (Vector3.Distance(transform.position, path[currentPathNode].position) < pathNodeProximityTolerance)
        {
            path[currentPathNode].visited = true;
            // Debug.Log(Time.frameCount + " >> " + path[currentPathNode].position + " visited");
            GetNextPathNode();
            agent.SetDestination(path[_currentPathNode].position);
        }
    }

    public void StopPatrolling()
    {
        agent.isStopped = true;
    }

    public void GeneratePath()
    {
        path = new PathNode[pathNodes];

        for (int i = 0; i < path.Length; i++)
        {
            Vector3 thisPosition = GenerateRandomVector3();
            PathNode thisPathNode = new PathNode();
            thisPathNode.position = thisPosition;
            path[i] = thisPathNode;
            // Instantiate(marker, thisPosition + Vector3.up, Quaternion.identity);
            // Debug.Log(Time.frameCount + " >> thisPosition [" + i + "]" + thisPosition);
        }

        if (path.Length > 0)
        {
            _currentPathNode = 0;
        }

        agent.SetDestination(path[_currentPathNode].position);
    }

    Vector3 GenerateRandomVector3()
    {
        float x1 = Random.value * minX;
        float x2 = Random.value * maxX;
        float z1 = Random.value * minZ;
        float z2 = Random.value * maxZ;

        Vector3 position = new Vector3((x1 + x2) / 2, transform.position.y, (z1 + z2) / 2);

        if (!CheckIfAccessible(position))
        {
            position = GenerateRandomVector3();
        }

        return position;
    }

    bool CheckIfAccessible(Vector3 position)
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, position, NavMesh.AllAreas, navMeshPath))
        {
            return true;
        }
        return false;
    }

    public void GetNextPathNode()
    {
        if (pathType == PathType.Sequential)
        {
            CalculateSequentialPath();
            return;
        }
        else if (pathType == PathType.Straight)
        {
            CalculateStraightPath();
        }
        else if (pathType == PathType.Short)
        {
            CalculateShortestPath();
        }
    }

    void CalculateSequentialPath()
    {
        _currentPathNode++;
        _currentPathNode %= path.Length;
    }

    void CalculateStraightPath()
    {
        List<int> distances = new List<int>();
        int next = currentPathNode;
        distances.Add(100);

        for (int i = 0; i < path.Length; i++)
        {
            if (!path[i].visited)
            {
                NavMeshPath tracedPath = new NavMeshPath();
                NavMesh.CalculatePath(path[currentPathNode].position, path[i].position, NavMesh.AllAreas, tracedPath);
                distances.Add(tracedPath.corners.Length);
                // Debug.Log(Time.frameCount + " >> distances[" + i + "]: " + distances[i]);
                if (distances[i] < distances[0])
                {
                    next = i;
                }
            }
        }

        // if ended up at the same place, clear visited
        if (currentPathNode == next)
        {
            ClearVisitedNodes();
        }

        _currentPathNode = next;
        // Debug.Log(Time.frameCount + " >> newCurrentNodeSet: " + currentPathNode);
    }

    void CalculateShortestPath()
    {
        List<float> distances = new List<float>();
        int next = currentPathNode;
        distances.Add(100);

        for (int i = 0; i < path.Length; i++)
        {
            if (!path[i].visited)
            {
                distances.Add(Vector3.Distance(path[currentPathNode].position, path[i].position));
                // Debug.Log(Time.frameCount + " >> distances[" + i + "]: " + distances[i]);
                if (distances[i] < distances[0])
                {
                    next = i;
                }
            }
        }

        // if ended up at the same place, clear visited
        if (currentPathNode == next)
        {
            ClearVisitedNodes();
        }

        _currentPathNode = next;
        // Debug.Log(Time.frameCount + " >> newCurrentNodeSet: " + currentPathNode);
    }

    void ClearVisitedNodes()
    {
        for (int i = 0; i < path.Length; i++)
        {
            path[i].visited = false;
        }
    }

    public Vector3 GetCurrentNodePosition()
    {
        return path[currentPathNode].position;
    }

    public int currentPathNode
    {
        get { return _currentPathNode; }
    }
}
