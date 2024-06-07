using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public NextLevelPopus GOP; 
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(nextLevel); // Add listener for button click
    }

    void nextLevel()
    {
        GOP.NextLevel();
    }
}
