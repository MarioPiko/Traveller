using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Herné nastavenia")]
    public int lifes;
    public int arrowsCount;
    public bool canEnterLocation = false;

    public int completedLevel = 1;
    public int savedGame;
    public bool finalDialogue = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        savedGame = PlayerPrefs.GetInt("completedLevel");
    }

    private void Start()
    {  
        Application.targetFrameRate = 300;

        if (savedGame == 0)
        {
            PlayerPrefs.SetInt("completedLevel", completedLevel);
        }
        else 
        {
            completedLevel = savedGame;
        }
    }

    public void TakeLife(int value)
    {
        lifes -= value;

        if (lifes <= 0)
        {
            UIManager.instance.GameOverUI();
            UIManager.instance.CheckLifes();
            GameOver();
        }
        else 
        {
            UIManager.instance.CheckLifes();
        }
    }

    public void AddLife(int value) 
    {
        lifes += value;
        UIManager.instance.CheckLifes();
    }

    public void AddArrow(int value) 
    {
        arrowsCount += value;
        UIManager.instance.UpdateArrowCountUI(arrowsCount);
    }

    public void GameOver() 
    {
       PlayerPrefs.SetInt("completedLevel", 1);
        lifes = 3;
        arrowsCount = 3;
    }

    public void LevelCompleted(int level) 
    {
        if (level > completedLevel && PlayerPrefs.GetInt("Victory") != 1) 
        {
            Debug.Log(PlayerPrefs.GetInt("Victory"));
            PlayerPrefs.SetInt("completedLevel", level);
        }
    }

}
