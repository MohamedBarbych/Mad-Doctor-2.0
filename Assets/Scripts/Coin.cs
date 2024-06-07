using UnityEngine;

public class Coin : MonoBehaviour
{
    public void Collect()
    {
        // Add your coin collection logic here, like increasing the player's score
        Debug.Log("Coin collected!");
        Destroy(gameObject); // Or disable the coin object
    }
}
