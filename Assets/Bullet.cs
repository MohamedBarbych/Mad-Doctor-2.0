using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 10; // Default damage that this bullet inflicts

    // Method to set the damage value dynamically, if needed
    public void SetDamage(int value)
    {
        damage = value;
    }

    // Trigger method to handle collisions
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet collided with: " + other.name);

        // Check if the collided object has the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Inflicting damage on enemy: " + other.name);

            // Get the Meander component and apply damage to the enemy
            Meander enemy = other.GetComponent<Meander>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Call TakeDamage on the Meander script
            }
            else
            {
                Debug.LogError("Meander component is missing on this enemy.");
            }
        }

        // Destroy the bullet after impact, regardless of the target
        Destroy(gameObject);
    }
}
