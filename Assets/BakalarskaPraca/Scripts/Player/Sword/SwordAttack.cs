using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [Header("Referencie")]
    [SerializeField] private GameObject swordPoint;

    [Header("Vstupné parametre")]
    [SerializeField] private float swordDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    public bool attackCompleted = false;

    [Header("Vrstva ktorú môže meč zasiahnuť")]
    public LayerMask hitableLayer;

    // Pomocou tejto metódy odštartujeme Unity Coroutine
    public void StartAttack() 
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack() 
    {
        Collider2D[] attackables = Physics2D.OverlapCircleAll(swordPoint.transform.position, attackRange, hitableLayer);

        foreach (Collider2D hit in attackables)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>().TakeDamage(swordDamage);
            }
            else if (hit.CompareTag("Destroyable"))
            {
                hit.GetComponent<Destroyable>().DestroyDestroyable();
            }
        }

        attackCompleted = true;
        yield return new WaitForSeconds(attackCooldown);
        attackCompleted = false;
    }

}
