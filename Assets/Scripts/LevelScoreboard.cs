using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScoreboard : MonoBehaviour
{
    // Start is called before the first frame update

public void LoadBackScene()
    {
        SceneManager.LoadScene(1);
    }
   public void LoadEasyLevel()
   {
       ScoreboardManager.level = "easylevel";
       SceneManager.LoadScene(5);
   }
   public void LoadAverageLevel()
   {
       ScoreboardManager.level = "averagelevel";
        SceneManager.LoadScene(5);
   }
   public void LoadDifficult()
   {
        ScoreboardManager.level = "difficultlevel";
        SceneManager.LoadScene(5);
   }
}
