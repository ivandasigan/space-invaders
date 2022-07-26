using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{

    public InputField inputUsername;
    public Button buttonSubmit;
    [SerializeField]
    public Text userScore;

  
    private string username;
   
    public static int userscore;
    public static string level;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("userscore " + userscore);
        Debug.Log("level " + level);
        userScore.text = userscore.ToString();
    }

    public void OnTextValueChange()
    {
        Debug.Log("input field " + inputUsername.text);
        username = inputUsername.text;
        if (username == "")
        {
            buttonSubmit.interactable = false;
            buttonSubmit.image.color = Color.gray;
            ///ColorBlock colors = buttonSubmit.colors;
           // buttonSubmit.backgroundColor = colors;
        } else {
            buttonSubmit.interactable = true;

            //ColorBlock colors = buttonSubmit.colors;
            buttonSubmit.image.color = new Color32(45, 162, 233,255);

        }
    }

    public void LoadBackScene()
    {
        SceneManager.LoadScene(3);
    }
    public void SubmitButton()
    {
        SaveOnDatabase();
    }
    private void SaveOnDatabase()
    {
      var newUser = new User(inputUsername.text, userscore);
      DatabaseHandler.PostUser(newUser, level, () =>
      {
        Debug.Log("Succes saving data!");
        SceneManager.LoadScene(2);
      });
    }

}
