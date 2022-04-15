using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Referencie na herné objekty UI")]
    public GameObject[] objectiveHint;
    public GameObject[] livesUI;

    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;
    public GameObject gameOverUI;
    public GameObject victoryUI;

    [Header("Referencie na komponenty")]
    [SerializeField] private TextMeshProUGUI arrowCountText;

    private int currentLevel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        CheckLifes();
        UpdateArrowCountUI(GameManager.instance.arrowsCount);
    }

    public void PauseGame()
    {
        SetTime(0);
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        SetTime(1);
        pauseMenuUI.SetActive(false);
    }

    public void MainMenu() 
    {
        SetTime(1);
        SceneManager.LoadScene(0);
    }

    public void StartAgain() 
    {
        SceneManager.LoadScene(1);
        GameManager.instance.lifes = 3;
    }

    public void RestartLevel() 
    {
        SetTime(1);
        SceneManager.LoadScene(currentLevel);
    }

    public void NextLevel() 
    {
        SceneManager.LoadScene(currentLevel + 1);
    }

    public void DeathUI() 
    {
        deathMenuUI.SetActive(true);
    }

    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    public void Victory() 
    {
        victoryUI.SetActive(true);
        PlayerPrefs.SetInt("Victory", 1);
    }
    public void CheckLifes()
    {
        for (int index = 3; index > GameManager.instance.lifes; index--) 
        {
            if (index > 0) 
            {
                livesUI[index - 1].SetActive(false);
            }
        }
    }

    public void UpdateArrowCountUI(int value) 
    {
        if (value <= 0) 
        {
            value = 0;
        }

        arrowCountText.text = value.ToString();
    }

    public void SetTime(int time) 
    {
        Time.timeScale = time;
    }
}
