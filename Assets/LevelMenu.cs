using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{

    public Button[] buttons;
    public GameObject levelButton;

    // private void Awake(){
    //     int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel" , 1);
    //     for(int i=0 ; i< buttons.Length ; i++){
            
    //         buttons[i].interactable = false;

    //     }
    //     for(int i=0 ; i< unlockedLevel ; i++){
            
    //         buttons[i].interactable = true;

    //     }
    // } 

    public void OpenLevel(int levelId){
        
        String levelname = "Level " + levelId;
        SceneManager.LoadScene(levelname);


    }

    void ButtonsToArray(){
        int childCount = levelButton.transform.childCount ; 
        buttons = new Button[childCount];
        for(int i=0 ; i< childCount ; i++){
            buttons[i] = levelButton.transform.GetChild(i).gameObject.GetComponent<Button>();
        }


    }
}
