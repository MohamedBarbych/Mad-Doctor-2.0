using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{
    public GameObject panel;
    public float delayBeforePopup = 0f; 

    void Start()
    {
        panel.SetActive(false);
    }

    public void ShowGameOverPopup()
    {
        panel.SetActive(true);

        //StartCoroutine(ShowPopupWithDelay());
    }

    private IEnumerator ShowPopupWithDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforePopup);

        // Show the game over popup
    }

    public void loadMenu()
    {
        StartCoroutine(LoadMenuScene());
    }

    public void RestartGame()
    {
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        // Wait for 2 seconds before reloading the scene
        yield return new WaitForSeconds(delayBeforePopup);

        // Destroy A* Pathfinding and PlaceBlocks instances
        Destroy(AStarPathfinding.Instance.gameObject);
        Destroy(PlaceBlocks.Instance.gameObject);

        yield return null;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // After the scene is loaded, call OnSceneReload on PlayerController
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.OnSceneReload();
        }

        // Unsubscribe from the event to avoid multiple calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator LoadMenuScene()
    {
        // Destroy all persistent singletons and game objects
        Destroy(AStarPathfinding.Instance.gameObject);
        Destroy(PlaceBlocks.Instance.gameObject);

        yield return null;

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
