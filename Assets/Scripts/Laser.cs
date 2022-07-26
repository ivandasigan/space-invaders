using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    //speed of variable

    private float speed = 8f;
    private bool _isEnemyLaser = false;
    
    [SerializeField]
    private GameObject _enemyGameObject;
    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;
    // Update is called once  

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("audio source in laser is null");
        } else 
        {
            _audioSource.clip = _explosionClip;
        }
        if (_enemyGameObject == null)
        {
            Debug.LogError("Enemy object in laser is null");
        }
    }

    void Update()
    {

   

        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();

        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);   
        if (transform.position.y >= 7f)
        {
            Destroy(this.gameObject);
        }

    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -7f)
        {
 
            Destroy(this.transform.parent.gameObject);
        }
    }
    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }
    private void OnTriggerEnter2D(Collider2D others)
    {
        if (others.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = others.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("player is hit");
                player.damage();
                _audioSource.Play();
            }
            Destroy(this.gameObject, 2.0f);
        }
    }
}
