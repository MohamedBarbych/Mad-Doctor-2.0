using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager Instance;

    public Slider playerHealthSlider;
    public Text enemyDeathCountText;
    public Text coinCountText;

    private int enemyDeathCount = 0;
    private int coinCount;

    void Awake()
    {
        //*************************   Singleton setup  **************************
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void UpdatePlayerHealth(int currentHealth)
    {
        if (playerHealthSlider != null)
            playerHealthSlider.value = currentHealth;
    }

    public void IncrementEnemyDeathCount()
    {
        enemyDeathCount++;
        Debug.Log("Enemy died, count now " + enemyDeathCount); // This will confirm the method is called
        if (enemyDeathCountText != null)
            enemyDeathCountText.text = "Enemies Killed: " + enemyDeathCount;
        else
            Debug.LogError("EnemyDeathCountText is not assigned in the Inspector!");
    }


    public void UpdateCoinCount(int newCoinCount)
    {
        coinCount = newCoinCount; // Ensure this updates the actual count properly

        if (coinCountText != null)
            coinCountText.text = "Coins: " + coinCount;
    }


    public void ResetUI()
    {
        ResetEnemyDeathCount();
        ResetCoinCount();
    }

    public void ResetEnemyDeathCount()
    {
        enemyDeathCount = 0;
        if (enemyDeathCountText != null)
            enemyDeathCountText.text = "Enemies Defeated: " + enemyDeathCount;
    }

    private void ResetCoinCount()
    {
        coinCount = 0;
        if (coinCountText != null)
            coinCountText.text = "Coins: " + coinCount;
    }



}
