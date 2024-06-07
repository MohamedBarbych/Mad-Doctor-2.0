using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour, IResettable
{
    public float speed = 5.0f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public List<Node> path;
    private int pathIndex;
    private GameObject[] coins;
    private GameObject targetCoin;
    private bool manualControl = true;
    private Vector2 moveInput;
    private PathVisualizer pathVisualizer;
    PlayerController playerController = new PlayerController();
    private CircleCollider2D circleCollider;


    void Awake()
    {
        //ResetManager.Instance.RegisterResettable(this);
    }

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        circleCollider = GetComponent<CircleCollider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pathVisualizer = GetComponent<PathVisualizer>();
        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        if (manualControl)
        {
            MoveManually();
        }
        else
        {
            MoveAutomatically();
        }
    }

    private void MoveManually()
    {
        if (circleCollider != null)
        {
            circleCollider.isTrigger = false;
        }

        playerController.PlayerManualMouvement();
    }

    private void MoveAutomatically()
    {
        if (circleCollider != null)
        {
            circleCollider.isTrigger = true;
        }

        rb.velocity = Vector2.zero;

        if (path == null || pathIndex >= path.Count) return;

        Vector3 targetPosition = new Vector3(path[pathIndex].gridPosition.x, path[pathIndex].gridPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            pathIndex++;
        }

        if (targetCoin != null)
        {
            FlipSprite(targetCoin.transform.position.x > transform.position.x);
        }
    }

    private IEnumerator UpdatePath()
    {
        while (true)
        {
            if (!manualControl)
            {
                coins = GameObject.FindGameObjectsWithTag("Coin");
                if (coins.Length > 0)
                {
                    targetCoin = FindClosestCoin();
                    if (targetCoin != null)
                    {
                        path = AStarPathfinding.Instance.FindPath(transform.position, targetCoin.transform.position);
                        pathIndex = 0;
                        pathVisualizer.DrawPath(path, true);
                    }
                }
                else
                {
                    targetCoin = null;
                    pathVisualizer.ClearEnemyPath();
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    private GameObject FindClosestCoin()
    {
        GameObject closestCoin = null;
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject coin in coins)
        {
            float distanceToCoin = Vector3.Distance(transform.position, coin.transform.position);
            if (distanceToCoin < shortestDistance)
            {
                shortestDistance = distanceToCoin;
                closestCoin = coin;
            }
        }
        return closestCoin;
    }

    private void FlipSprite(bool flip)
    {
        spriteRenderer.flipX = flip;
    }

    public void ToggleMovementMode()
    {
        manualControl = !manualControl;
        rb.velocity = Vector2.zero;
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        path = null;
        targetCoin = null;
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
