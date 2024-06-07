using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowAStar : MonoBehaviour, IResettable
{
    public Transform player;
    public float speed = 3.0f;
    public float detectionRange = 10.0f;
    public float stopDistance = 1.5f;
    private SpriteRenderer spriteRenderer;
    public List<Node> path;
    private int pathIndex;
    private PathVisualizer pathVisualizer;

    void Awake()
    {
        //ResetManager.Instance.RegisterResettable(this);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pathVisualizer = GetComponent<PathVisualizer>();
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if (path == null || pathIndex >= path.Count) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            Vector3 targetPosition = new Vector3(path[pathIndex].gridPosition.x, path[pathIndex].gridPosition.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                pathIndex++;
            }

            FlipSprite(player.position.x > transform.position.x);
        }
    }

    private IEnumerator UpdatePath()
    {
        while (true)
        {
            if (player != null)
            {
                path = AStarPathfinding.Instance.FindPath(transform.position, player.position);
                pathIndex = 0;
                pathVisualizer.DrawPath(path, false);
            }
            else
            {
                pathVisualizer.ClearEnemyPath();
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void FlipSprite(bool flip)
    {
        spriteRenderer.flipX = flip;
    }

    public void Reset()
    {
        path = null;
        pathIndex = 0;
    }

    public void OnSceneReload()
    {
        StartCoroutine(UpdatePath());
    }

    void OnDestroy()
    {
        if (ResetManager.Instance != null)
        {
            ResetManager.Instance.UnregisterResettable(this);
        }
    }
}
