using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [Header("Referencia na animátor")]
    [SerializeField] Animator enemyAnimator;
    
    public bool inRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
           inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }
}
 