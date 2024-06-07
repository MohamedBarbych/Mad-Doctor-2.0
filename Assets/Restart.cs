using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameOverPopup GOP;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(restart);
    }

    void OnEnable()
    {
        if (button != null)
        {
            button.onClick.AddListener(restart);
        }
    }

    void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(restart);
        }
    }

    void restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        GOP.RestartGame();
    }
}
