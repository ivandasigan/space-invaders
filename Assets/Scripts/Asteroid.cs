using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 3.0f;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("audio source in asteroid is NULL ");
        } else
        {
            _audioSource.clip = _explosionClip;
        }


    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
        
            _audioSource.Play();
            GameObject newExplosion = Instantiate(_explosion, transform.position, Quaternion.identity);
     
            AsteroidDamage();
            Destroy(other.gameObject);
            Destroy(newExplosion, 2.0f);
        }
        if (other.tag == "Player")
        {
            Player _player = other.GetComponent<Player>();
            if (_player != null) {
                _player.damage();
                _audioSource.Play();
    
                AsteroidDamage();
            }
        }
    }

    public void AsteroidDamage()
    {
        _lives--;
        if (_lives == 0) {
            Destroy(this.gameObject, 0.25f);
        } 
    }


}
