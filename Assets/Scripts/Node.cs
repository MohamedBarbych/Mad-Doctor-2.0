using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3Int gridPosition;
    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost => gCost + hCost;

    public Node(bool walkable, Vector3Int gridPosition)
    {
        this.walkable = walkable;
        this.gridPosition = gridPosition;
    }
}
