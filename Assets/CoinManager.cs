using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }
    public string coinTag = "Coin";

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

    private void Update()
    {
        CheckCoins();
    }

    private void CheckCoins()
    {
        if (PlaceBlocks.coinCount == 0)
        {
            StartCoroutine(ReloadScene());
        }
    }
    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(0);

        // Destroy A* Pathfinding and PlaceBlocks instances
        Destroy(AStarPathfinding.Instance.gameObject);
        Destroy(PlaceBlocks.Instance.gameObject);

        yield return null;

        SceneManager.LoadScene(3);

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

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
   
}
