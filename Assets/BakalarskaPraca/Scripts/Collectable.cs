using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("Typ predmetu ktorý môže hráč zbierať")]
    [SerializeField] private string item;

    // Skontrolujeme či je kolízia s hráčom, ak ano vykonaj danú akciu
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (item.Equals("life"))
            {
                Destroy(this.gameObject);
                GameManager.instance.AddLife(1);
            }
            else if (item.Equals("arrow"))
            {
                Destroy(this.gameObject);
                GameManager.instance.AddArrow(1);
            }
        }
    }
}
