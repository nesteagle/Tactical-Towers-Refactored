using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class Pathfinding
{
    public static List<Node> FindPath(Node startPoint, Node endPoint)
    {
        if (startPoint == null || endPoint == null)
        {
            return new List<Node>(); // Failure
        }
        List<Node> openPath = new();
        HashSet<Node> closedPath = new();

        Node current = startPoint;

        current.G = 0;
        current.H = HexMapUtil.GetDistance(startPoint.Position, endPoint.Position);
        openPath.Add(current);

        while (openPath.Count != 0)
        {
            openPath = openPath.OrderBy(x => x.F).ThenByDescending(x => x.G).ToList();
            current = openPath[0];

            openPath.Remove(current);
            closedPath.Add(current);

            if (current == endPoint)
            {
                return GetFinalPath(startPoint, endPoint, closedPath);
            }

            foreach (Node adjacent in current.GetEdges())
            {
                if (adjacent == null || adjacent.Occupied || closedPath.Contains(adjacent)) continue;

                int g = current.G + adjacent.Weight + 1; // Consider the weight between the current tile and the adjacent tile.

                if (!openPath.Contains(adjacent))
                {
                    adjacent.G = g;
                    adjacent.H = HexMapUtil.GetDistance(adjacent.Position, endPoint.Position);
                    openPath.Add(adjacent);
                }
                else if (adjacent.G > g)
                {
                    adjacent.G = g;
                }
            }
        }

        return new List<Node>(); // Failure
    }

    private static List<Node> GetFinalPath(Node startPoint, Node endPoint, HashSet<Node> closedPath)
    {
        List<Node> finalPath = new();

        Node current = endPoint;

        while (current != startPoint)
        {
            if (!current.Occupied)
            {
                finalPath.Add(current);
            }
            Node nextTile = null;
            int lowestG = int.MaxValue;

            foreach (Node adjacentTile in current.GetEdges())
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


}