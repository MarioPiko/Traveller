using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    [Header("Available levels")]
    [SerializeField] private Button[] levelButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("completedLevel");

        for (int index = 0; index < levelButtons.Length; index++) 
        {
            if (index + 1 > levelReached) 
            {
                levelButtons[index].interactable = false;
            }
        }          
    }    
}

