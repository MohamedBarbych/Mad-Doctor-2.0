using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<GameObject> enemies = new List<GameObject>();

    private void Awake()
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetGame()
    {
        // Reset any necessary game elements here
    }

    // New methods for enemy management
    public void RegisterEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
        Debug.Log("Enemy registered. Total enemies: " + enemies.Count);
    }

    public void DeregisterEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Debug.Log("Enemy deregistered. Total enemies: " + enemies.Count);
        CheckForLevelCompletion();
    }

    private void CheckForLevelCompletion()
    {
        if (enemies.Count == 0)
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        Debug.Log("All enemies killed! Level Completed!");
        UnlockNewLevel();
        // Load the next level (make sure you have the next scene set up in the build settings)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void UnlockNewLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 0);
        if (currentSceneIndex >= reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", currentSceneIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
