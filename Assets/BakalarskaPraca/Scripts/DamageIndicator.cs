using System.Collections;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [Header("Potrebné referencie na komponenty")]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Nastavenie hodnôt indikátora zranenia")]
    public int blinkAmount = 1;
    public float blinkTimer = 0.2f;

    public void GetHit() 
    {
        StartCoroutine(Blink());   
    }

    IEnumerator Blink()
    {
        for (int i = 0; i < blinkAmount; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkTimer);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkTimer);
        }
    }
}
