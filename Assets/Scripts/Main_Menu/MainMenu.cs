using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void loadGame()

    {
        SceneManager.LoadScene(2); // main game
    }
    public void loadTutorial()
    {
        SceneManager.LoadScene(4);
    }
    public void loadScoreboard()
    {
        SceneManager.LoadScene(6);
    }
    public void CloseGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    public void About()
    {
        SceneManager.LoadScene(9);
    }
}
