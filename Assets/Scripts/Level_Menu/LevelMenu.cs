using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Enemy _enemyGameObject;
    [SerializeField]
    private SpawnManager _spawnManagerGameObject;
    [SerializeField]
    private BossEnemy _bossEnemyGameObject;
    void Start()
    {
     //   _enemyGameObject = GameObject.Find("Enemy").GetComponent<Enemy>();
        if (_enemyGameObject == null)
        {
            Debug.LogError("Enemy in Level menu scene is null");
        } 
        if (_spawnManagerGameObject == null) {
            Debug.LogError("Spawn manager in level menu is null");
        }
        if (_bossEnemyGameObject == null)
        {
            Debug.LogError("Enemy boss in level menu is null");
        }
    }
    
    public void Back()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadAverageLevel()
    {
        _enemyGameObject.SetEnemyAverageLevel();
       _bossEnemyGameObject.SetBossEnemyAverageLevel();
        SceneManager.LoadScene(3);
        _spawnManagerGameObject.SetEnemyDelay(2.0f);    
        _spawnManagerGameObject.SetAsteroidDelay(12.0f); 
        UIManager.level = "averagelevel";
    }
    public void LoadEasyLevel()
    {
        _enemyGameObject.SetEnemyBeginnerLevel();
        _bossEnemyGameObject.SetBossEnemyBeginnerLevel();
        SceneManager.LoadScene(3);
        _spawnManagerGameObject.SetEnemyDelay(4.0f);
        _spawnManagerGameObject.SetAsteroidDelay(15.0f);
        UIManager.level = "easylevel";
    }
    public void LoadDifficultLevel()
    {
     _enemyGameObject.SetEnemyAverageLevel();
     _bossEnemyGameObject.SetBossEnemyDifficultLevel();
        SceneManager.LoadScene(3);
        _spawnManagerGameObject.SetEnemyDelay(2.0f);    
        _spawnManagerGameObject.SetAsteroidDelay(8.0f); 
   
        UIManager.level = "difficultlevel";
    }
}
