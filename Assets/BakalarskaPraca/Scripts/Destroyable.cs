using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [Header("Referencie na kopmonenty a objekty")]
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] spawnableObjects;

    public void DestroyDestroyable() 
    {
        animator.SetTrigger("destroy");
        Destroy(boxCollider);

        if(GameManager.instance.lifes == 3)
        {
            Instantiate(spawnableObjects[0], spawnPoint.position, Quaternion.identity);
        }
        else 
        {
            int randomItem = Random.Range(0, spawnableObjects.Length);
            Instantiate(spawnableObjects[randomItem], spawnPoint.position, Quaternion.identity);
        }
    }
}
