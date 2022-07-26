using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 5.5f;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private GameObject _laserPrehab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _canIn = -1f;
    private float _delay = 0.3f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    private bool _isTripleShotAtive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private int _score;
    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;

    private UIManager _uiManager;
    void Start()
    {

        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Audio source in player is NULL");
        } else
        {
            _audioSource.clip = _laserSoundClip;
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canIn)
        {
            fireLaser();
        }

    }
    void calculateMovement()
    {
        
 

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;
        transform.Translate(direction);


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.908349f, 5.978068f), 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.200919f, 9.208622f), transform.position.y, 0);

    }

    void fireLaser()
    {
        _canIn = Time.deltaTime + _delay;
        if (_isTripleShotAtive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
         else
        {
              Instantiate(_laserPrehab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        _audioSource.Play();
      
    }
    public void damage()
    {

        if (_isShieldActive == true)
        {
            _shieldVisualizer.SetActive(false);
            _isShieldActive = false;
            return;
        }

        _lives--;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        } else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

      
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            Instantiate(_explosion, transform.position, Quaternion.identity);
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
        bool isSpawnBoss = _spawnManager._spawnBossEnemy;
        if (_score == 100 )
        {
            Debug.Log("Showwwww bosss");
            _spawnManager.OnPlayerDeath();
            _spawnManager.OnSpawnBossEnemy();
            _uiManager.ShowBossEnemyLifeBar();
        }
    }

    public void tripleShotActive()
    {
        _isTripleShotAtive = true;
        StartCoroutine(TripleShotDownRoutine());
    }

    public void shieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldRoutine());
    }

    IEnumerator ShieldRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isShieldActive = false;
    }
    IEnumerator TripleShotDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotAtive = false;
    }

 
}
