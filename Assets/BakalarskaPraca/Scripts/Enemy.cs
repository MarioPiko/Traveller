using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [Tooltip("Referencie na komponenty")]
    [SerializeField] DamageIndicator damageIndicator;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] DetectPlayer detectPlayer;
    private PlayerController player;

    [Tooltip("Základné nastavenia")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float followRange;
    [SerializeField] private float timer;
    [SerializeField] private bool isBoss = false;
    private float distanceFromPlayer;


    int direction;


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (player != null && player.playerAlive)
        {
            distanceFromPlayer = Vector2.Distance(this.transform.position, player.gameObject.transform.position);
           
            if (distanceFromPlayer < attackRange)
            {
                animator.SetBool("isMoving", false);
                if (timer <= 0) 
                {
                    AttackPlayer();
                    timer = 1.5f;
                }
            }
            else if (distanceFromPlayer < followRange)
            {
                FollowPlayer();
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    public void AttackPlayer() 
    {
        animator.SetTrigger("enemyAttack");
    }

    public void FollowPlayer() 
    {
        if (player.gameObject.transform.position.x < transform.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        transform.Translate(transform.right * direction * movementSpeed * Time.deltaTime);
        FlipEnemy();
    }

    public void FlipEnemy() 
    {
        if (player.gameObject.transform.position.x < transform.position.x)
        {
            if (transform.localScale.x > 0)
            {
                if (!isBoss)
                {
                    transform.localScale = new Vector3(-1f, 1f, 0f);
                }
                else 
                {
                    transform.localScale = new Vector3(-2.5f, 2.5f, 0f);
                }
     
            }
        }
        else
        {
            if (transform.localScale.x < 0)
            {
                if (!isBoss)
                {
                    transform.localScale = new Vector3(1f, 1f, 0f);
                }
                else 
                {
                    transform.localScale = new Vector3(2.5f, 2.5f, 0f);
                }

            }
        }

    }

    public void TakeDamage(float damageValue) 
    {
        damageIndicator.GetHit();
        health -= damageValue;

        if (health <= 0) 
        {
            EnemyDie();
        }
    }

    public void DealDamageToPlayer()
    {
        if (detectPlayer.inRange) 
        {
            player.GetHurt();
        }

    }

    public void EnemyDie() 
    {
        Destroy(this.gameObject);

        if (isBoss) 
        {
            GameManager.instance.canEnterLocation = true;
            FindObjectOfType<NPC>().GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}
