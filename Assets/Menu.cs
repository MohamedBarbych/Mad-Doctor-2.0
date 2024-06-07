using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameOverPopup GOP;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(menu);
    }

    void OnEnable()
    {
        if (button != null)
        {
            button.onClick.AddListener(menu);
        }
    }

    void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(menu);
        }
    }

    void menu()
    {
        GOP.loadMenu();
    }
}
