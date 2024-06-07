using UnityEngine;
using UnityEngine.UI;

public class ShowPanelOnClick : MonoBehaviour
{
    public Button yourButton; // Reference to your Button
    public GameObject panel; // Reference to your Panel

    void Start()
    {
        // Ensure the panel is initially hidden
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // Add a listener to the button to execute the ShowPanel method when clicked
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(ShowPanel);
        }
    }

    void ShowPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }
}
