using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Vstupné parametre")]
    [SerializeField] private float arrowDamage = 50f;

    // V tejto časti si porovnáme Tagy a podla toho spustíme požadovanú akciu
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(arrowDamage);
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Destroyable")) 
        {
            collision.GetComponent<Destroyable>().DestroyDestroyable();
            Destroy(this.gameObject);
        }
        else if (collision.CompareTag("Wall") || collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
