using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
        public void StartGame()
    {
        //Destroy(AStarPathfinding.Instance.gameObject);
        //Destroy(PlaceBlocks.Instance.gameObject);

        SceneManager.LoadScene(1);  


    }
    



}
