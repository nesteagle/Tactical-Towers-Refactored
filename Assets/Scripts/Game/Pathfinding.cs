using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class Pathfinding
{
    public static List<Pathable> FindPath(Pathable startPoint, Pathable endPoint)
    {
        List<Pathable> openPath = new();
        HashSet<Pathable> closedPath = new();

        Pathable current = startPoint;

        current.G = 0;
        current.H = GetEstimatedPathCost(startPoint.Position, endPoint.Position);
        openPath.Add(current);

        while (openPath.Count != 0)
        {
            openPath = openPath.OrderBy(x => x.F).ThenByDescending(x => x.G).ToList();
            current = openPath[0];

            openPath.Remove(current);
            closedPath.Add(current);

            if (current == endPoint)
            {
                return GetFinalPath(startPoint,endPoint,closedPath);
            }

            foreach (Pathable adjacent in current.GetEdges())
            {
                if (adjacent == null || adjacent.Occupied || closedPath.Contains(adjacent)) continue;

                int g = current.G + adjacent.Weight + 1; // Consider the weight between the current tile and the adjacent tile.

                if (!openPath.Contains(adjacent))
                {
                    adjacent.G = g;
                    adjacent.H = GetEstimatedPathCost(adjacent.Position, endPoint.Position);
                    openPath.Add(adjacent);
                }
                else if (adjacent.G > g)
                {
                    adjacent.G = g;
                }
            }
        }

        return new List<Pathable>(); // Failure
    }

    private static List<Pathable> GetFinalPath(Pathable startPoint, Pathable endPoint, HashSet<Pathable> closedPath)
    {
        List<Pathable> finalPath = new();

        Pathable current = endPoint;

        while (current != startPoint)
        {
            if (!current.Occupied)
            {
                finalPath.Add(current);
            }
            Pathable nextTile = null;
            int lowestG = int.MaxValue;

            foreach (Pathable adjacentTile in current.GetEdges())
            {
                if (adjacentTile == null || !closedPath.Contains(adjacentTile))
                {
                    continue;
                }
                if (adjacentTile.G < lowestG)
                {
                    lowestG = adjacentTile.G;
                    nextTile = adjacentTile;
                }
            }

            current = nextTile;
        }
        finalPath.Add(startPoint);
        finalPath.Reverse();
        return finalPath;
    }

    public static int GetEstimatedPathCost(Vector3Int startPosition, Vector3Int targetPosition)
    {
        return Mathf.Max(Mathf.Abs(startPosition.z - targetPosition.z), Mathf.Max(Mathf.Abs(startPosition.x - targetPosition.x), Mathf.Abs(startPosition.y - targetPosition.y)));
    }

    public static int GetEstimatedPathCost(Axial startPosition, Axial targetPosition)
    {
        return GetEstimatedPathCost(startPosition.ToCubic(), targetPosition.ToCubic());
    }
}