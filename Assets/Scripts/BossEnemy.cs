using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private GameObject _bossEnemyLaserPrefab;
    [SerializeField]
    private GameObject _bossEnemyTwoLaserPrefab;
    [SerializeField]
    private GameObject _bossEnemyShieldPrefab;
    [SerializeField]
    private float _lives = 800.0f;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;
    [SerializeField]
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private int direction = 1;
    
    private UIManager _uiManager;
    
    [SerializeField]
    private int _levelInteger = 0;
    private int _damage = 10;

    public static bool _isBossShieldActive = true;
    // Start is called before the first frame update
    void Start()
    {
        if (_bossEnemyLaserPrefab == null)
        {
            Debug.LogError("boss enemy laser is null");
        }
         _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
         if (_uiManager == null)
         {
             Debug.LogError("UIManager is null");
         }

         _audioSource = GetComponent<AudioSource>();
         if (_audioSource != null) 
         {
             _audioSource.clip = _explosionClip;
         }
         if (_bossEnemyTwoLaserPrefab == null)
         {
             Debug.Log("Boss enemy laser is null");
         }
       
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Time.time > _canFire)
            {
                _fireRate = Random.Range(3.0f, 7.0f);
                _canFire = Time.time + _fireRate;

                if (_levelInteger == 0) {
                    Instantiate(_bossEnemyLaserPrefab, transform.position, Quaternion.identity);
                } else if (_levelInteger == 1)
                {
                    Debug.Log("two laser boss ..... average");
                    Instantiate(_bossEnemyTwoLaserPrefab, transform.position, Quaternion.identity);
                } else if (_levelInteger == 2)
                {
                    Debug.Log("two laser boss ..... difficult");
                    GameObject twolaser = Instantiate(_bossEnemyTwoLaserPrefab, transform.position, Quaternion.identity);
                    if (twolaser == null)
                    {
                        Debug.Log("towlaser is null");
                    } else
                     {
                         Debug.Log("two laser is not null");
                    }
                }
                
                
            }

    }

    private void CalculateMovement()
    {
        if (transform.position.y > 4.0)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
         
        if (transform.position.x > 8.0) {
         direction = -1;
        }
        else if (transform.position.x < -8.0) {
            direction = 1;
        }
        Vector3 movement = Vector3.right * direction * _speed * Time.deltaTime; 
        transform.Translate(movement); 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Player _player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
                _player.damage();
            }
            Debug.Log("Boss enemy hit player");
        }
        if (other.tag == "Laser")
        {
            GameObject newExplosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            if (_lives <= 0)
            {
                _uiManager.AddPointsFromBoss();
                _uiManager.GameOverSequence();
                Destroy(this.gameObject);
            }
            if (_levelInteger == 2 && _isBossShieldActive == true &&  _lives < 400) {
                _bossEnemyShieldPrefab.SetActive(true);
            }
            _lives-=_damage;
            _uiManager.UpdateBossLives(_lives);
            _audioSource.Play();
            Destroy(newExplosion, 2.0f);
            Destroy(other.gameObject);

        }
        
    }

    public void SetBossEnemyBeginnerLevel()
    {
        _levelInteger = 0;
        _damage = 40;
    }
    public void SetBossEnemyAverageLevel()
    {
        _levelInteger = 1;
        _damage = 30;
    }
    public void SetBossEnemyDifficultLevel()
    {
        _levelInteger = 2;
        _damage = 100;
    }
}
