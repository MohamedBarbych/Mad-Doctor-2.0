using System.Collections.Generic;
using UnityEngine;

public class EnemyPathManager : MonoBehaviour
{
    private List<PathVisualizer> enemyPathVisualizers = new List<PathVisualizer>();

    public void Register(PathVisualizer visualizer)
    {
        if (!enemyPathVisualizers.Contains(visualizer))
        {
            enemyPathVisualizers.Add(visualizer);
        }
    }

    public void Unregister(PathVisualizer visualizer)
    {
        if (enemyPathVisualizers.Contains(visualizer))
        {
            enemyPathVisualizers.Remove(visualizer);
        }
    }

    public void ToggleAllEnemyPaths(bool isVisible)
    {
        foreach (var visualizer in enemyPathVisualizers)
        {
            visualizer.ToggleEnemyPathVisibility(isVisible);
        }
    }
}
