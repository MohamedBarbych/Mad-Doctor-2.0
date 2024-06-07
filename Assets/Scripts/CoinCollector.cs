using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoinCollector : MonoBehaviour
{
    public Transform player;
    public KeyCode collectKey = KeyCode.Keypad1; // Keypad key "1"
    private bool isInRoom = false;
    private bool isCollecting = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRoom = true;
            Debug.Log("Player entered the room.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRoom = false;
            Debug.Log("Player exited the room.");
            StopAllCoroutines(); // Stop collecting when the player leaves the room
            isCollecting = false;
        }
    }

    void Update()
    {
        if (isInRoom && Input.GetKeyDown(collectKey) && !isCollecting)
        {
            StartCoroutine(CollectCoins());
        }
    }

    private IEnumerator CollectCoins()
    {
        isCollecting = true;
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag("Coin");

        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

        foreach (GameObject coinObject in coinObjects)
        {
            agent.SetDestination(coinObject.transform.position);
            Debug.Log($"Moving to coin at position {coinObject.transform.position}");

            // Wait until the player reaches the coin
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            Coin coin = coinObject.GetComponent<Coin>();
            if (coin != null)
            {
                coin.Collect(); // Assuming Coin has a Collect method
                Debug.Log("Coin collected.");
            }

            yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        }

        Debug.Log("Finished collecting coins in the room.");
        isCollecting = false;
    }
}
