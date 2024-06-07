using System.Collections;
using UnityEngine;

public class CoinChecker : MonoBehaviour
{
    public NextLevelPopus nlP;
    private bool coinsGenerated = false;

    void Start()
    {
        PlaceBlocks.Instance.OnCoinsGenerated.AddListener(StartCheckingForCoins);
    }

    void StartCheckingForCoins()
    {
        StartCoroutine(WaitForCoinsGeneration());
    }

    IEnumerator WaitForCoinsGeneration()
    {
        while (true)
        {
            GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

            if (coins != null && coins.Length > 0)
            {
                coinsGenerated = true;
                break;
            }

            yield return new WaitForSeconds(0.5f);

            if (coinsGenerated)
            {
                StartCoroutine(CheckForCoins());
            }
        }

        IEnumerator CheckForCoins()
        {
            while (true)
            {
                GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

                if (coins == null || coins.Length == 0)
                {
                    nlP.ShowNextLevelPanel();
                    yield break;
                }

                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}