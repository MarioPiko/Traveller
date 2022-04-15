using UnityEngine;

public class BowAttack : MonoBehaviour
{
    [Header("Referencia na script PlayerController")]
    private PlayerController playerController;

    [Header("Vstupné parametre")]
    [SerializeField] private float arrowSpeed = 200f;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowStart;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Na pozícii luku si vytvoríme inštanciu nového šípu a metódu ShootArrow() voláme priamo z animátora
    // pre lepšie načasovanie releasu
    public void ShootArrow()
    {
            GameObject releasedArrow = Instantiate(arrow, arrowStart.position, arrow.transform.rotation);
            GameManager.instance.arrowsCount -= 1;
            UIManager.instance.UpdateArrowCountUI(GameManager.instance.arrowsCount);

            if (playerController.facingRight)
            {
                releasedArrow.GetComponent<Rigidbody2D>().AddForce(Vector2.right * arrowSpeed);
                releasedArrow.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (!playerController.facingRight)
            {
                releasedArrow.GetComponent<Rigidbody2D>().AddForce(Vector2.left * arrowSpeed);
                releasedArrow.GetComponent<SpriteRenderer>().flipX = true;
            }
    }
}
