using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    public Tilemap emptyMap;
    public Tilemap wallMap;
    public int gridSize = 50;

    private Node[,] nodes;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateGrid()
    {
        nodes = new Node[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3Int gridPosition = new Vector3Int(x, y, 0);
                bool walkable = emptyMap.GetTile(gridPosition) != null && wallMap.GetTile(gridPosition) == null;
                nodes[x, y] = new Node(walkable, gridPosition);
            }
        }
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector3Int gridPosition = emptyMap.WorldToCell(worldPosition);
        return nodes[gridPosition.x, gridPosition.y];
    }

    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridPosition.x + x;
                int checkY = node.gridPosition.y + y;

                if (checkX >= 0 && checkX < gridSize && checkY >= 0 && checkY < gridSize)
                {
                    neighbors.Add(nodes[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    public IEnumerable<Node> GetAllNodes()
    {
        foreach (var node in nodes)
        {
            yield return node;
        }
    }

    public void ResetGrid()
    {
        CreateGrid();
    }
}
