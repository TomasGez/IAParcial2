using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder<T>
{
    public List<T> AStarSearch(T startingNode, T goalNode, Func<T, List<T>> GetNeighbors, Func<T, int> GetDangerNodeCost, Func<T, Vector3> GetPosition, bool precise)
    {
        PriorityQueue<T> frontier = new();
        frontier.Enqueue(startingNode, 0);

        Dictionary<T, T> cameFrom = new();
        cameFrom.Add(startingNode, default);

        Dictionary<T, int> costSoFar = new();
        costSoFar.Add(startingNode, 0);

        HashSet<T> processed = new();

        T current;

        while(frontier.Count != 0)
        {
            current = frontier.Dequeue();
            if (!processed.Add(current)) continue;

            if (current.Equals(goalNode)) return ReconstructPath(cameFrom, current);

            foreach(T neighbor in GetNeighbors(current))
            {
                int newCost = costSoFar[current] + GetDangerNodeCost(current);
                if (cameFrom.ContainsKey(neighbor) && newCost >= costSoFar[neighbor]) continue;

                frontier.Enqueue(neighbor, newCost + GetHeuristic(GetPosition(neighbor), GetPosition(goalNode), precise));
                cameFrom[neighbor] = current;
                costSoFar[neighbor] = newCost;
                processed.Remove(neighbor);
            }
        }

        return new();
    }

    private List<T> ReconstructPath(Dictionary<T, T> cameFrom, T current)
    {
        List<T> path = new();

        while (current != null)
        {
            path.Add(current);
            current = cameFrom[current];
        }

        path.Reverse();

        return path;
    }

    private float GetHeuristic(Vector3 a, Vector3 b, bool precise)
    {
        float dx = Mathf.Abs(a.x - b.x);
        float dz = Mathf.Abs(a.z - b.z);

        if (precise) return Mathf.Sqrt((dx * dx) + (dz * dz));

        return (dx * dx) + (dz * dz);
    }
}
