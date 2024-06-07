using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayContoller : MonoBehaviour
{
    public static GamePlayContoller Instance;

    [SerializeField]
    private Text enemyKillCountTxt;

    private int enemyKillCount;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

    }

    public void EnemyKilled()
    {
        enemyKillCount++;
        enemyKillCountTxt.text = "Enemies Killed: " + enemyKillCount;

    }

    public void RestartGameee()
    {
        Invoke("Restart", 3f);
    }

    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

}
