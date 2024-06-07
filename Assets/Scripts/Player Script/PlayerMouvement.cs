//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.InteropServices;
//using UnityEngine;

//public class PlayerMouvement : MonoBehaviour
//{

//    [SerializeField]
//    private float moveSpeed = 3.5f;

//    [SerializeField]
//    private float minBound_X = -71f, maxBound_X = 71f, minBound_Y = -3.3f, maxBound_Y = 0f;

//    private Vector3 temPos;
//    private float xAxis, yAxis;
//    private PlayerAnimations playerAnimation;

//    [SerializeField]
//    private float ShootWaitTime = 0.5f;//Bullet per 0.5 seconds
//    private float waitBeforeShooting;

//    [SerializeField]
//    private float moveWaitTime = 0.3f;
//    private float waitBeforeMoving;
//    private bool canMove = true;
//    private PlayerShootingManager playershootingManager;
//    private bool playerDied;

//    private void Awake()
//    {
//        playerAnimation = GetComponent<PlayerAnimations>();
//        playershootingManager = GetComponent<PlayerShootingManager>();
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        if (playerDied)
//            return;
//        Mouvement();
//        HandleAnimation();
//        HandleFacingDirection();

//        HandleShooting();   
//        CheckIfCanMove();
//    }

//    void Mouvement()
//    {
//        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
//        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

//        if(!canMove)
//        {
//            return;
//        }

//        temPos = transform.position;
//        //Mouvement config
//        temPos.x += xAxis * moveSpeed * Time.deltaTime;//to smooth the mouvement
//        temPos.y += yAxis * moveSpeed * Time.deltaTime;
//        //*********************  Checking the Bounce  ****************************  
//        if (temPos.x < minBound_X)
//            temPos.x = minBound_X;//Go back to the min position in case of passing it

//        if (temPos.x > maxBound_X)
//            temPos.x = maxBound_X;//Same thing if we passed the max position

//        if (temPos.y < minBound_Y)
//            temPos.y = minBound_Y;//Go back to the min position in case of passing it

//        if (temPos.y > maxBound_Y)
//            temPos.y = maxBound_Y;//Same thing if we passed the max position

//        transform.position = temPos;    
        
//    }

//    void HandleAnimation()
//    {
//        if (!canMove)
//        {
//            return;
//        }

//        if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
//            playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
//        else
//            playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);

//    }
//     //To handle the face direction orientation with arrows left and right
//    void HandleFacingDirection()
//    {
//        if (xAxis > 0)
//            playerAnimation.setFacingDirection(true);
//        else if(xAxis < 0)
//            playerAnimation.setFacingDirection(false);
//    }

//    void StopMovement()
//    {
//        canMove = false;
//        waitBeforeMoving = Time.time + moveWaitTime;
//    }

//    void Shoot()
//    {
//        waitBeforeShooting = Time.time + ShootWaitTime;
//        StopMovement();
//        playerAnimation.PlayAnimation(TagManager.SHOOT_ANIMATION_NAME);

//        playershootingManager.Shoot(transform.localScale.x);
//    }

//    void CheckIfCanMove()
//    {
//        if(Time.time > waitBeforeMoving)
//            canMove = true;
//    }

//    void HandleShooting()  
//    {
//        if (Input.GetKey(KeyCode.LeftControl))
//        {
//            if (Time.time > waitBeforeShooting)
//                Shoot();
//        }

//    }


//    public void PlayerDied()
//    {
//        playerDied = true;
//        playerAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
//        Invoke("DestroyPlayerAfterDelay", 2f);

//    }

//    void DestroyPlayerAfterDelay()
//    {
//        Destroy(gameObject); 
//    }
     
//}

 
