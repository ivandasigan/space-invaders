using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class User 
{
    

    public int userscore;
    public string username;
    
    public User(string name, int score) 
    {
        this.userscore = score;
        this.username = name;
    }

}
