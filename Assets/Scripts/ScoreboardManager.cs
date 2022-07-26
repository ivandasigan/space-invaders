using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreboardManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static List<User> myusers = new List<User>();

    public static string level;

    void Start()
    {
        Debug.Log("selected level ==> " + level);
        myusers.Clear();
        GameObject textTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        DatabaseHandler.GetUsers(level, users => {
            foreach (var user in users)
            {
              //  users = user.Value;
               Debug.Log($"{user.Value}");

               User userss = new User(user.Value.username.ToString(), user.Value.userscore);
               myusers.Add(userss);

            }
              Debug.Log("user len ===== " + myusers.Count);
              for (int i = 0; i < myusers.Count; i++)
              {
                g = Instantiate(textTemplate, transform);
                g.transform.GetChild(0).GetComponent<Text>().text =  myusers[i].username;
                g.transform.GetChild(1).GetComponent<Text>().text =  myusers[i].userscore.ToString();
              }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

