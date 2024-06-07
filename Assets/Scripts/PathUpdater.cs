using System.Collections;
using UnityEngine;

public class PathUpdater : MonoBehaviour
{
    public EnemyFollowAStar enemyFollow;
    public PlayerCoinCollector playerCoinCollector;
    public PathVisualizer playerPathVisualizer;
    public PathVisualizer enemyPathVisualizer;

    void Start()
    {
        enemyFollow = GetComponent<EnemyFollowAStar>();
        playerCoinCollector = GetComponent<PlayerCoinCollector>();
        playerPathVisualizer = playerCoinCollector.GetComponent<PathVisualizer>();
        enemyPathVisualizer = enemyFollow.GetComponent<PathVisualizer>();
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while (true)
        {
            if (enemyFollow != null && enemyPathVisualizer != null)
            {
                enemyPathVisualizer.DrawPath(enemyFollow.path, false); // false indicates enemy path
            }
            if (playerCoinCollector != null && playerPathVisualizer != null)
            {
                playerPathVisualizer.DrawPath(playerCoinCollector.path, true); // true indicates player path
            }
            yield return new WaitForSeconds(0.1f); // Update the path every 0.1 seconds
        }
    }
}
