using System.Collections.Generic;
using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    private LineRenderer playerLineRenderer;
    private LineRenderer enemyLineRenderer;
    public float lineWidth = 0.1f;
    public Color playerLineColor = Color.green;
    public Color enemyLineColor = Color.red;
    public string playerSortingLayerName = "PlayerPath";
    public string enemySortingLayerName = "EnemyPath";
    public int playerSortingOrder = 10;
    public int enemySortingOrder = 8;

    private EnemyPathManager enemyPathManager;

    void Awake()
    {
        playerLineRenderer = CreateLineRenderer(playerLineColor, playerSortingLayerName, playerSortingOrder);

        enemyLineRenderer = CreateLineRenderer(enemyLineColor, enemySortingLayerName, enemySortingOrder);
    }

    void Start()
    {
        enemyPathManager = FindObjectOfType<EnemyPathManager>();
        if (enemyPathManager != null)
        {
            enemyPathManager.Register(this);
        }
    }

    void OnDestroy()
    {
        if (enemyPathManager != null)
        {
            enemyPathManager.Unregister(this);
        }
    }

    private LineRenderer CreateLineRenderer(Color color, string sortingLayerName, int sortingOrder)
    {
        GameObject lineObject = new GameObject("LineRenderer");
        lineObject.transform.SetParent(transform);
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.useWorldSpace = true;
        lineRenderer.sortingLayerName = sortingLayerName;
        lineRenderer.sortingOrder = sortingOrder;
        return lineRenderer;
    }

    public void DrawPath(List<Node> path, bool isPlayerPath)
    {
        LineRenderer lineRenderer = isPlayerPath ? playerLineRenderer : enemyLineRenderer;
        if (path == null || path.Count == 0)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        lineRenderer.positionCount = path.Count;
        for (int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(path[i].gridPosition.x, path[i].gridPosition.y, 0));
        }
    }

    public void ClearPlayerPath()
    {
        playerLineRenderer.positionCount = 0;
    }

    public void ClearEnemyPath()
    {
        enemyLineRenderer.positionCount = 0;
    }

    public void TogglePlayerPathVisibility(bool isVisible)
    {
        playerLineRenderer.enabled = isVisible;
    }

    public void ToggleEnemyPathVisibility(bool isVisible)
    {
        enemyLineRenderer.enabled = isVisible;
    }
}
