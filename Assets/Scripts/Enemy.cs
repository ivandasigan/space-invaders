using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _enemy_OneLaser;
    [SerializeField]
    private GameObject _enemy_TwoLasers;

    private Player _player;
    private Animator _anim;

    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;

    private float _fireRate = 3.0f;
    private float _canFire = -1;

    [SerializeField]
    private bool _isBeginnerLevel = false;
    [SerializeField]
    private int _enemyLife = 1;

  
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
        	
            Debug.LogError("The player is null");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animation is null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio source in Enemy is NULL");
        }
        else
        {
            _audioSource.clip = _explosionClip;
        }
        Debug.Log("Enemy Life: " + _enemyLife);
    }

    // Update is called once per frame
    void Update()
    {
       
            CalculateMovement();
            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(3.0f, 7.0f);
                _canFire = Time.time + _fireRate;

                if (_isBeginnerLevel == true)
                {
                    Debug.Log("value : " + _isBeginnerLevel);
                    GameObject enemyLaser = Instantiate(_enemy_OneLaser, transform.position, Quaternion.identity);
                    Laser lasers = enemyLaser.GetComponentInChildren<Laser>();
                    lasers.AssignEnemyLaser();
               
                }
                else
                {
                    Debug.Log("value : " + _isBeginnerLevel);
                    GameObject enemyLaser = Instantiate(_enemy_TwoLasers, transform.position, Quaternion.identity);
                    Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

                    for (int i = 0; i < lasers.Length; i++)
                    {
                        lasers[i].AssignEnemyLaser();
                    }
                }

            
        }
      
    }

    private void CalculateMovement()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomX, 6, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("On Collision");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("outside");
        if (other.tag == "Player")
        {

            //damage player
            Player _player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
                _player.damage();
            }
            _audioSource.Play();
            _anim.SetTrigger("OnEnemyDeath");

            _speed = 0;
            Destroy(this.gameObject, 2.6f);
            Debug.Log("Player hit");
        }
        
        if (other.tag == "Laser")
        {
         
            
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _audioSource.Play();
            _anim.SetTrigger("OnEnemyDeath");
            
   
                _speed = 0;
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2.6f);
        }
     
    }
    public void SetEnemyBeginnerLevel()
    {
      
        _isBeginnerLevel = true;
        _enemyLife = 1;
      
    }
    public void SetEnemyAverageLevel()
    {
    
        _isBeginnerLevel = false;
        _enemyLife = 2;
     
    }

    

}
