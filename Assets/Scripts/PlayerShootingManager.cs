//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerShootingManager : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject bulletPrefab;

//    [SerializeField]
//    private Transform bulletSpawnPos;

//    public void Shoot(float facingDirection)
//    {
//        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
//        if (facingDirection < 0)
//        {
//            newBullet.GetComponent<Bullets>().setNegativeSpeed();

//        }

//        SoundManager.instance.PlayShootSound();

//    }

//}
