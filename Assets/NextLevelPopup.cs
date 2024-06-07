using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPopus : MonoBehaviour
{
    public GameObject nextLevelPanel;

    void Start()
    {
        nextLevelPanel.SetActive(false);
    }

    public void ShowNextLevelPanel()
    {
        nextLevelPanel.SetActive(false);
    }

    public void NextLevel()
    {
        StartCoroutine(ReloadLevel());
    }

    private IEnumerator ReloadLevel()
    {
        Destroy(AStarPathfinding.Instance.gameObject);
        Destroy(PlaceBlocks.Instance.gameObject);

        yield return null;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if (currentSceneIndex == 2)
        {
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator LoadMenuScene()
    {
        Destroy(AStarPathfinding.Instance.gameObject);
        Destroy(PlaceBlocks.Instance.gameObject);

        yield return null;

        SceneManager.LoadScene(0);
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
