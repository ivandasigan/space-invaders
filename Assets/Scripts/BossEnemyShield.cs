using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyShield : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool _isActive = false;
    public int _shieldLife = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Laser")
        {
            Debug.Log("boss shield is hit");
            Destroy(other.gameObject);
            _shieldLife-=15;
            if (_shieldLife <= 0) 
            {
                BossEnemy._isBossShieldActive = false;
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
}
