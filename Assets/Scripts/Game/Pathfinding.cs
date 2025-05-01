using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Pathfinding : MonoBehaviour
{
    public static List<Pathable> FindPath(Pathable startPoint, Pathable endPoint)
    {

        List<Pathable> openPathTiles = new();
        List<Pathable> closedPathTiles = new();

        Pathable currentTile = startPoint;

        currentTile.G = 0;
        currentTile.H = GetEstimatedPathCost(AxialToCubic(startPoint.Position), AxialToCubic(endPoint.Position));
        openPathTiles.Add(currentTile);

        while (openPathTiles.Count != 0)
        {
            openPathTiles = openPathTiles.OrderBy(x => x.F).ThenByDescending(x => x.G).ToList();
            currentTile = openPathTiles[0];

            openPathTiles.Remove(currentTile);
            closedPathTiles.Add(currentTile);

            if (currentTile == endPoint)
            {
                return GetFinalPath();
            }

            foreach (Pathable adjacentTile in currentTile.GetEdges())
            {
                if (adjacentTile == null || closedPathTiles.Contains(adjacentTile)) continue;
                if (adjacentTile.Occupied)
                {
                    if (adjacentTile == endPoint)
                    {
                        List<Pathable> path = GetFinalPath();
                        return path.Take(path.Count - 1).ToList();
                    }
                    continue;
                }

                int g = currentTile.G + 1 + adjacentTile.Weight; // Consider the weight between the current tile and the adjacent tile.

                if (!(openPathTiles.Contains(adjacentTile)))
                {
                    adjacentTile.G = g;
                    adjacentTile.H = GetEstimatedPathCost(AxialToCubic(adjacentTile.Position), AxialToCubic(endPoint.Position));
                    openPathTiles.Add(adjacentTile);
                }
                else if (adjacentTile.G > g)
                {
                    adjacentTile.G = g;
                }
            }
        }

        List<Pathable> GetFinalPath()
        {
            List<Pathable> finalPathTiles = new List<Pathable>();

            Pathable cTile = endPoint;

            while (cTile != startPoint)
            {
                finalPathTiles.Add(cTile);
                Pathable nextTile = null;
                int lowestG = int.MaxValue;

                foreach (Pathable adjacentTile in cTile.GetEdges())
                {
                    if (adjacentTile == null || !closedPathTiles.Contains(adjacentTile))
                    {
                        continue;
                    }
                    if (adjacentTile.G < lowestG)
                    {
                        lowestG = adjacentTile.G;
                        nextTile = adjacentTile;
                    }
                }

                cTile = nextTile;
            }
            finalPathTiles.Add(startPoint);
            finalPathTiles.Reverse();
            return finalPathTiles;
        }

        return null; // Failure  
    }

    protected static int GetEstimatedPathCost(Vector3Int startPosition, Vector3Int targetPosition)
    {
        return Mathf.Max(Mathf.Abs(startPosition.z - targetPosition.z), Mathf.Max(Mathf.Abs(startPosition.x - targetPosition.x), Mathf.Abs(startPosition.y - targetPosition.y)));
    }

    public static Vector3Int AxialToCubic(Vector2Int axial)
    {
        return new Vector3Int(axial.x, axial.y, -axial.x - axial.y);
    }
}