using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Meander : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;
    private Transform playerTransform;
    public GameObject damageArea;
    public int maxHealth = 50;
    public int health = 50;
    public float detectionRange = 15f;
    public float attackRange = 2f;
    public float deathDelay = 0.5f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = -1f;
    private bool isDead = false;

    public Slider healthBarSlider;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Start()
    {
        InitializeEnemy();
        // Register this enemy with the GameManager
        GameManager.Instance.RegisterEnemy(this.gameObject);
    }

    public void InitializeEnemy()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Cannot find the player GameObject!");
        }

        health = maxHealth;
        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = health;
        }

        if (damageArea != null)
        {
            damageArea.SetActive(false);
        }

        isDead = false;
        animator.SetBool("isDead", false);
        agent.enabled = true;
    }

    void Update()
    {
        if (isDead || playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance < attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            agent.SetDestination(transform.position);
            animator.SetBool("isWalking", false);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
        else if (distance < detectionRange)
        {
            agent.SetDestination(playerTransform.position);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void TriggerAttack()
    {
        if (!isDead && damageArea != null)
        {
            damageArea.SetActive(true);
        }
    }

    public void EndAttack()
    {
        if (damageArea != null)
        {
            damageArea.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }

        if (healthBarSlider != null)
        {
            healthBarSlider.value = health;
        }
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        agent.enabled = false;
        Destroy(gameObject, deathDelay);
        EndAttack();
        
        // Deregister this enemy with the GameManager
        GameManager.Instance.DeregisterEnemy(this.gameObject);
        Debug.Log("Enemy died and deregistered: " + gameObject.name); // Add debug log
    }
}
