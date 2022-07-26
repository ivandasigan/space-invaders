using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using Proyecto26;

public class Scoreboard : MonoBehaviour
{

    public Text scoreText;
    public InputField inputName;
    public InputField inputScore;
    public Text retrievescoreText;

    public static string playerName;
    public static int playerScore ;

    // private System.Random random = new System.Random();
    //User users = new User();


    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    // private static void OnAppStart()
    // {
    //     DatabaseHandler.GetUsers( users => {
    //         foreach (var user in users)
    //         {
    //             Debug.Log($"{user.Value.userscore}");
    //         }
    //     });
    // }





    // Start is called before the first frame update
    void Start()
    {


    }

    public void OnSubmit()
    {
      var newUser = new User(inputName.text, int.Parse(inputScore.text));
      DatabaseHandler.PostUser(newUser, "averagelevel", () =>
      {
        Debug.Log("Succes saving data!");
      });
    }
    
    public void OnRetrieve()
    {
        // DatabaseHandler.GetUsers( users => {
        //     foreach (var user in users)
        //     {
        //         Debug.Log($"{user.Value.userscore}");
        //     }
        // });
    }

    private void UpdateScore()
    {
      //  retrievescoreText.text = users.userscore.ToString();
    }

    // private void PostToDatabase()
    // {
    //     User user = new User();
    //     RestClient.Post("https://space-invader-3327b-default-rtdb.firebaseio.com/" + playerName + ".json", user) ;
    // }
    // private void RetrieveFromDatabase()
    // {
    //     RestClient.Get<User>("https://space-invader-3327b-default-rtdb.firebaseio.com/" + inputName.text + ".json").Then(
    //         response => {
    //             users = response;
    //             UpdateScore();
    //         });
 
    // }
    public void LoadBackScene()
    {
        SceneManager.LoadScene(6);
    }
}
