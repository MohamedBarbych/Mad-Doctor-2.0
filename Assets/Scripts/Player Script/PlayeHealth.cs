//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//public class PlayeHealth : MonoBehaviour
//{
//    [SerializeField]
//    private float health = 100f;

//    private PlayerMouvement playerMouvement;

//    [SerializeField]    
//    private Slider healthSlider;    

//    private void Awake()
//    {
//        playerMouvement = GetComponent<PlayerMouvement>();  
//    }

//    public void TakeDamage(float damageAmount)
//    {
//        if (health <= 0f)
//            return;
//        health -= damageAmount;
//        if(health <= 0)
//        {
//            //Informing that the player died
//            playerMouvement.PlayerDied();

//            GamePlayContoller.Instance.RestartGameee();

//        }

//        healthSlider.value = health;    
//    }


//}
 