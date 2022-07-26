using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _bossEnemyPrefab;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _asteroidPrefab;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] powerups;

    [SerializeField]
    private float _delayAsteroid = 4.0f;
    [SerializeField]
    private float _delayEnemy = 8.0f;
    [SerializeField]
    public bool _spawnBossEnemy = false;
    
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnShieldRoutine());
        StartCoroutine(SpawnAsteriod());

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError(" UI manager in spawnmanager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    // spawn game objects every 5 seconds
    // create coroutine of type IEnumerator --yield events
    // while loop

    IEnumerator spawnEnemyRoutine()
    {

        while (_stopSpawning == false)
        {
        
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 posToSpawn = new Vector3(randomX, 6, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;  
            yield return new WaitForSeconds(Random.Range(_delayEnemy, _delayEnemy + 3.0f));
        }

         if(_spawnBossEnemy == true)
        {

            StartCoroutine(BossEnemyRoutine());
        }

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(6.0f, 8.0f));
        //spawn every 5 seconds with random position x
        while(_stopSpawning == false)
        { 
            float randomX = Random.Range(-8.0f, 8.0f);
            int randomIndex = Random.Range(0, 2);
            Vector3 posToSpawn = new Vector3(randomX, 6, 0);
      
            Instantiate(powerups[randomIndex], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6.0f, 10.0f));
        }
    }

     IEnumerator BossEnemyRoutine()
    {
        _uiManager.ShowReminderText();
        yield return new WaitForSeconds(6.0f);
        _uiManager.RemoveReminderText();
        float randomX = Random.Range(-8.0f, 8.0f);
        Vector3 posToSpawn = new Vector3(randomX, 9, 0);
        Instantiate(_bossEnemyPrefab, posToSpawn, Quaternion.identity);
    }
    IEnumerator SpawnShieldRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 60.0f));
        float randomX = Random.Range(-8.0f, 8.0f);
        Vector3 postToSpawn = new Vector3(randomX, 6, 0);
        Instantiate(_shieldPrefab, postToSpawn, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(8.0f, 12.0f));
    }
    public void OnPlayerDeath()
    {
        
        _stopSpawning = true;
    }
    public void OnSpawnBossEnemy()
    {
        _spawnBossEnemy = true;
    }
    public void OnStopSpawnBossEnemy()
    {
        _spawnBossEnemy = false;
    }
IEnumerator SpawnAsteriod()
{
        
        //float lastRange = _delay + 5.0f;

        while (_stopSpawning == false) 
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 postToSpawn = new Vector3(randomX, 6, 0);
            GameObject newAsteroid = Instantiate(_asteroidPrefab, postToSpawn, Quaternion.identity);
            newAsteroid.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(_delayAsteroid, _delayAsteroid + 5.0f));
        }
}
public void SetEnemyDelay(float delay) 
    {
        this._delayEnemy = delay;
        Debug.Log("Enemy Delay " + delay);
    }

public void SetAsteroidDelay(float delay) 
{
    this._delayAsteroid = delay;
    Debug.Log("Delay " + delay);
}

}
