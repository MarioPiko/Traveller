using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // Namiesto OpenMenu a CloseMenu sa dá aj použiť priamo Unity OnClick() event a nastavenie bool na aktívny alebo neaktívny
    // ale pre jednodtnosť a prehladnosť to zakomponujem do Menu scriptu


    public void OpenMenu(GameObject menuObject) 
    {
        menuObject.SetActive(true);
    }

    public void CloseMenu(GameObject menuObject) 
    {
        menuObject.SetActive(false);
    }

    public void LoadLevel(int levelNumber) 
    {
        SceneManager.LoadScene(levelNumber);
    }

    // Metóda na ukončenie aplikácie (nefunguje v Unity Editora iba priamo v builde)
    public void QuitGame() 
    {
        Application.Quit();
        Debug.Log("Vypínam hru!");
    }
}