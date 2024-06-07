using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    private float move, vMove;
    private bool keyJump, keyShoot;
    public float speed = 6.5f;
    private Rigidbody2D rb2d;
    public int health = 100;
    public int maxHealth = 100;
    public int coins = 0;
    public int jumpForce = 250;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public int bulletDamage = 10;

    public AudioClip coinSound;
    public AudioClip shootingSound;

    private Animator anim;
    private AudioSource audioSource;
    private Vector3 tempScale;
    private bool isDead = false;
    private bool facingRight = true; // To store the current facing direction

    public Slider healthSlider;
    private PlaceBlocks placeBlocks;

    public GameOverPopup gameOverPopup;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        health = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }

        placeBlocks = FindObjectOfType<PlaceBlocks>();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Ensure the player is not destroyed between scenes
    }

    void Update()
    {
        if (isDead) return;

        move = Input.GetAxisRaw("Horizontal");
        vMove = Input.GetAxisRaw("Vertical");
        keyJump = Input.GetButtonDown("Jump");
        keyShoot = Input.GetButtonDown("Fire1");

        PlayerManualMouvement();

        if(PlaceBlocks.coinCount <= 0){
            UnlockNewLevel();
        }
    }

    public void PlayerManualMouvement()
    {
        rb2d.velocity = new Vector2(move * speed, vMove * speed);

        if (Mathf.Abs(move) > 0 || Mathf.Abs(vMove) > 0)
        {
            PlayAnimation("walk");
            SetFacingDirection(move > 0);
        }
        else
        {
            PlayAnimation("idle");
        }

        if (keyShoot)
        {
            ShootBullet();
            PlayAnimation("Shoot");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameplayUIManager.Instance.UpdateCoinCount(coins++);
            //**********************************
            --PlaceBlocks.coinCount;

            if (coinSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(coinSound);
            }

        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
        GameplayUIManager.Instance.UpdatePlayerHealth(health);

        if (health <= 0 && !isDead)
        {
            Debug.Log("Player has died.");
            isDead = true;
            anim.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;

            // Start the coroutine to handle the death process
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        // Wait for 2 seconds (or the length of the death animation)
        yield return new WaitForSeconds(2f);

        // Destroy the player object after the delay
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }

        gameOverPopup.ShowGameOverPopup();
    }


    private void ShootBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            Vector2 shootingDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            bulletRb.velocity = shootingDirection * bulletSpeed;

            if (shootingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(shootingSound);
            }

            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.SetDamage(bulletDamage);
            }
        }
    }

    public void PlayAnimation(string animationName)
    {
        int animationHash = Animator.StringToHash(animationName);
        anim.Play(animationName);
    }

    public void SetFacingDirection(bool faceRight)
    {
        facingRight = faceRight; // Store the direction
        tempScale = transform.localScale;
        tempScale.x = faceRight ? Mathf.Abs(tempScale.x) : -Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;
    }

    public void OnSceneReload()
    {
        SetFacingDirection(facingRight); // Reset the direction on scene reload
    }
    //******************************               **********************************
    private void UnlockNewLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 0);
        if (currentSceneIndex >= reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", currentSceneIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}

public class GameOverScreen
{
    internal void Setup(int maxPlatform)
    {
        throw new NotImplementedException();
    }
}