using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlaceBlocks;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;       // Player's transform
    public float speed = 5.0f;     // Speed at which the enemy moves
    public float detectionRange = 10.0f;  // Distance within which the player is followed
    public float stopDistance = 1.5f;     // Minimum distance to stop from the player
    public Room myRoom;            // The room constraints
    private SpriteRenderer spriteRenderer; // Sprite renderer to flip the sprite

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer component
    }

    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection range and stop distance is maintained
        if (distanceToPlayer < detectionRange && distanceToPlayer > stopDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 newPosition = transform.position + directionToPlayer * speed * Time.deltaTime;

            // Ensure the new position is within the room boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, myRoom.Left, myRoom.Right);
            newPosition.y = Mathf.Clamp(newPosition.y, myRoom.Bottom, myRoom.Top);

            transform.position = newPosition;
        }

        // Flip the enemy sprite based on player position
        FlipSprite(player.position.x > transform.position.x);
    }

    private void FlipSprite(bool flip)
    {
        // Flip the sprite based on the direction to the player
        spriteRenderer.flipX = flip;
    }
}
