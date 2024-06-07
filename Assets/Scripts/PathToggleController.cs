using UnityEngine;
using UnityEngine.UI;

public class PathToggleController : MonoBehaviour
{
    public Button toggleEnemyPathButton;
    public Button togglePlayerPathButton;
    public PathVisualizer playerPathVisualizer;
    public EnemyPathManager enemyPathManager;

    private bool isEnemyPathVisible = true;
    private bool isPlayerPathVisible = true;

    void Start()
    {
        toggleEnemyPathButton.onClick.AddListener(ToggleEnemyPath);
        togglePlayerPathButton.onClick.AddListener(TogglePlayerPath);
    }

    void ToggleEnemyPath()
    {
        isEnemyPathVisible = !isEnemyPathVisible;
        if (enemyPathManager != null)
        {
            enemyPathManager.ToggleAllEnemyPaths(isEnemyPathVisible);
        }
    }

    void TogglePlayerPath()
    {
        isPlayerPathVisible = !isPlayerPathVisible;
        if (playerPathVisualizer != null)
        {
            playerPathVisualizer.TogglePlayerPathVisibility(isPlayerPathVisible);
        }
    }
}
