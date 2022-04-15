using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLocation : MonoBehaviour
{
    [Header("Referencie na UI")]
    [SerializeField] private GameObject actionUI;
    [SerializeField] private GameObject enterButton;

    [Header("Vstupné parametre")]
    [SerializeField] private int currentLevel;

    private int nextLevel; 


    private void Start()
    {
        nextLevel += currentLevel + 1;
        
        if (currentLevel == 1 || currentLevel == 3 || currentLevel == 5)
        {
            GameManager.instance.canEnterLocation = false;
        }
        else 
        {
            GameManager.instance.canEnterLocation = true;
        }

        if (currentLevel != 5)
        {
            GameManager.instance.finalDialogue = false;

        }
        else 
        {
            GameManager.instance.finalDialogue = true;
        }
    }

    public void EnterNewLocation() 
    {
        SceneManager.LoadScene(nextLevel);
        GameManager.instance.LevelCompleted(nextLevel);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.canEnterLocation) 
        {
            if (collision.CompareTag("Player"))
            {
                actionUI.SetActive(false);
                enterButton.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.instance.canEnterLocation)
            if (collision.CompareTag("Player"))
            {
                actionUI.SetActive(true);
                enterButton.SetActive(false);
            }
    }
   }

