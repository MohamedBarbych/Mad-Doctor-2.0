using UnityEngine;
using UnityEngine.UI;

public class MovementToggleButton : MonoBehaviour
{
    public PlayerCoinCollector playerCoinCollector; // Reference to the PlayerCoinCollector script
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleMovement); // Add listener for button click
    }

    void ToggleMovement()
    {
        playerCoinCollector.ToggleMovementMode();
    }
}
     