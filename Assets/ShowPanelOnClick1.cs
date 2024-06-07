using UnityEngine;
using UnityEngine.UI;

public class ShowPanelOnClick1 : MonoBehaviour
{
    public Button yourButton; // Reference to your Button
    public GameObject panel1; // Reference to your Panel
    public GameObject panel2; // Reference to your Panel

    void Start()
    {
        // Ensure the panel is initially hidden
        if (panel1 != null)
        {
            panel1.SetActive(false);
        }

        // Add a listener to the button to execute the ShowPanel method when clicked
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(ShowPanel);
        }
    }

    void ShowPanel()
    {
        if (panel1 != null && panel2 != null)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
        }
    }
}
