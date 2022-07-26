using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using FullSerializer;

public class DatabaseHandler 
{
    private static readonly string databaseUrl = "https://space-invader-3327b-default-rtdb.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();
    public delegate void PostUserCallback();
    public delegate void GetUsersCallback(Dictionary<string, User> users);
 
    private static User user;
    public static void GetUsers(string level, GetUsersCallback callback)
    {
        RestClient.Get($"{databaseUrl}users/{level}.json").Then(response => {
            var responseJson = response.Text;    
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);
            var users = deserialized as Dictionary<string , User>;

            callback(users);
        });
    }

    public static void PostUser(User user,string level, PostUserCallback callback)
    {
        RestClient.Put<User>($"{databaseUrl}users/{level}/{user.username}.json", user).Then(response => {
            Debug.Log("The use was successfully uploaded to the database");
            callback();
        });
    }

}


//   var users = JsonConvert.DeserializeObject<User>(responseJson);
//             var usersss = JsonConvert.DeserializeObject<dynamic>(responseJson);
//             //Debug.Log(usersss.ToString());
//             foreach (var user in usersss)
//             {
//                 var myUserrr = JsonConvert.DeserializeObject<User>(user);
//                 Debug.Log(user.user1.username);
//             }