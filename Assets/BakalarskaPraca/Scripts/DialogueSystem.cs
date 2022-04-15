using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [Header("Referencia na textové komponenty")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Referencia na objekty tlačidiel a panelu ktoré potrebujeme zapínať a vypínať")]
    [SerializeField] private GameObject touchController;
    [SerializeField] private GameObject dialoguePanel;

    [Header("Funkčné nastavenia dialógu")]
    public float textSpeed;
    private int index;

    [Header("Pole stringov pre mená a riadky v dialógu")]
    [SerializeField] private string[] dialogueNames;
    [SerializeField] private string[] dialogueLines;


    public void StartDialogue() 
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText() 
    {
        MobileController.instance.StopMovement();
        touchController.SetActive(false);
        dialoguePanel.SetActive(true);

        foreach (char name in dialogueNames[index].ToCharArray())
        {
            nameText.text += name;

        }

        foreach (char line in dialogueLines[index].ToCharArray()) 
        {
            dialogueText.text += line;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void TapToContinue() 
    {
        if (dialogueText.text == dialogueLines[index])
        {
            NextLine();
        }
        else 
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[index];
        }
    }

    public void EndDialogue() 
    {
        touchController.SetActive(true);
        dialoguePanel.SetActive(false);
    }

    private void NextLine() 
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            nameText.text = string.Empty;
            StartCoroutine(ShowText());
        }
        else 
        {
            EndDialogue();
            GameManager.instance.canEnterLocation = true;
            UIManager.instance.objectiveHint[1].SetActive(true);

            if (GameManager.instance.finalDialogue) 
            {
                UIManager.instance.Victory();
            }
        }
    }
}
