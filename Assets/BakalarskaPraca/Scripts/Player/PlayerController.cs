using System.Collections;
using UnityEngine;

public enum Weapon
{
    sword,
    bow
}

public class PlayerController : MonoBehaviour
{

    [Header("Referencie na komponenty")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] DamageIndicator damageIndicator;

    /* ------------------------------ Začiatok Sekcia - Nastavenie ovládania ------------------------------ */
    // Tooltip hodnoty sa zobrazujú v Editore a používajú sa hlavne pri práci v týme ako jednoduché vysvetlenie danej premennej a jej účel

    [Header("Sekcia - Nastavenie ovládania")]

    [Tooltip("Rýchlosť pohybu hráča")]
    [SerializeField] float movementSpeed;

    [Tooltip("Hodnota ako vysoko môže hráč vyskočiť")]
    [SerializeField] float jumpForce;

    [Tooltip("Hodnota ako vysoko môže hráč vyskočiť na dvojskok")]
    [SerializeField] float secondJumpForce;

    [Tooltip("Počet bonusových dvojskokov")]
    [SerializeField] int extraJumps;

    [Tooltip("Dostupné dvojskoky vo vzduchu")]
    private int availableJumps;

    [Tooltip("Hodnota ako daleko dosiahne kotúľ hráča")]
    [SerializeField] float rollForce;

    [Tooltip("Detekcia na akej vrstve sa náš objekt hráča práve pohybuje - Základné nastavenie je Zem")]
    [SerializeField] LayerMask groundLayer;

    [Tooltip("Smer otočenia Sprite hráča")]
    public bool facingRight;

    [Tooltip("Vráti true ak sa hráč pohybuje, bool hodnota sa používa pre animátor")]
    [SerializeField] bool isPlayerMoving;

    /* ------------------------------ Koniec Sekcia - Nastavenie ovládania ------------------------------ */

    [Header("Roll system")]
    [SerializeField] float dashDistance = 7f;
    public bool rollActive;
    [SerializeField] private float rollCooldown;
    [SerializeField] private float cooldownValue;

    [Header("Bojovy system")]
    [SerializeField] bool allowSwordAttack;
    public Weapon weapon;
    public bool playerAlive = true;
    public int blinkAmount = 1;
    public float blinkTimer = 0.2f;

    [Header("Stavy Animacii")]
    private string run = "isRunning";
    private string attack = "isAttacking";
    private string roll = "isRolling";
    private string fallSpeed = "isFallingSpeed";
    private string bowAttack = "BowAttack";
    private string die = "playerDie";
    private string rollCompleted = "isRollCompleted";



    private void Update()
    {
        // Ak hráč nežije tak dalej nepotrebujeme nič vykonávať
        if (!playerAlive) { return; }

        if (!rollActive) 
        {
            MovePlayer();
        }
       
        FlipSprite();
        DesktopInput();

        animator.SetFloat(fallSpeed, rigidBody.velocity.y);

        if (IsGrounded())
        {
            availableJumps = extraJumps;
        }


        if (rollCooldown <= 0) 
        {
            rollCooldown = 0;
            return;
        }
        rollCooldown -= Time.deltaTime;
    }

    private void Start()
    {
        facingRight = transform.localScale.x > 0;
        availableJumps = extraJumps;
        weapon = Weapon.sword;
    }

    public void MovePlayer()
    {
        if (MobileController.instance.moveLeft || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody.velocity = new Vector2(-movementSpeed, rigidBody.velocity.y);

        }
        else if (MobileController.instance.moveRight || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.velocity = new Vector2(movementSpeed, rigidBody.velocity.y);

        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            animator.SetBool("isIdle", true);
        }
       
        animator.SetBool(run, isPlayerMoving);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
        else if (!IsGrounded() && availableJumps > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, secondJumpForce);
            availableJumps--;
        }

    }

    public void Roll()
    {
        if (rollCooldown == 0) 
        {
            StartCoroutine(StartRoll(transform.localScale.x));
        }

    }

    public void GetHurt() 
    {
        GameManager.instance.TakeLife(1);

        if (GameManager.instance.lifes <= 0) 
        {
            animator.SetTrigger(die);
            playerAlive = false;
        }

        damageIndicator.GetHit();
    }

    IEnumerator StartRoll(float direction) 
    {
        rollActive = true;

        rigidBody.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        animator.SetTrigger(roll);

        yield return new WaitForSeconds(0.3f);
        rollActive = false;
        animator.SetTrigger(rollCompleted);
        rollCooldown = cooldownValue;
    }


    private void FlipSprite()
    {
        isPlayerMoving = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;

        // Problém nstane, že po uvolení tlačítka inputu sa Sprite automaticky otočí doprava pretože rigidBody.velocity.x je brané ako kladná hodnota preto to potrebujeme ošetrit

        if (isPlayerMoving)
        {
            // Mathf.Sign nám vracia hodnotu 1 ak je hodnota f pozitívna alebo 0 a -1 ked f je negatívna

            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1f);
            facingRight = transform.localScale.x > 0;
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.
            BoxCast(
            boxCollider.bounds.center, 
            boxCollider.bounds.size, 
            0f, Vector2.down, 0.1f, groundLayer);
    }


    public void ChangeWeapon() 
    {
        switch (weapon) 
        {
            case Weapon.sword:
                weapon = Weapon.bow;
                animator.SetBool("isBow", true);
                break;
            case Weapon.bow:
                weapon = Weapon.sword;
                animator.SetBool("isBow", false);
                break;
        }
    }

    public void SwordAttack()
    {
        if (allowSwordAttack)
        {
            animator.SetTrigger(attack);
        }
    }

    public void BowAttack() 
    {
        if (GameManager.instance.arrowsCount > 0)
        {
            animator.SetTrigger(bowAttack);
        }
        else 
        {
            GameManager.instance.arrowsCount = 0;
        }
    }
        
    // Tu si nastavíme zvyšne tlačidlá pre testovanie Inputu v prostredí Unity Editora
    public void DesktopInput() 
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            Jump();
        }
       
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(StartRoll(transform.localScale.x));
        }
       
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            SwordAttack();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            BowAttack();
        }
    }
}