using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Referencie")]
    [SerializeField] DialogueSystem dialogue;
    [SerializeField] GameObject questHint;

    [Header("Nastavenia dialogu")]
    private bool canSpeak;

    private void Start()
    {
        canSpeak = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if (canSpeak) 
            {
                dialogue.StartDialogue();
                canSpeak=false;
                UIManager.instance.objectiveHint[0].SetActive(false);
            }

        }
    }
}
