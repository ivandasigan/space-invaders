using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{

    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;
    private float speed = 8.0f;
    public bool _isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio source is null in boss enemy laser");
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
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -7f)
        {
            Debug.Log("Destroy Boss Enemy laser");
            Destroy(this.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Player _player = other.transform.GetComponent<Player>();
            if (_player != null) 
            {
                _audioSource.Play();
                _player.damage();
            }
        }
    }
 
}
