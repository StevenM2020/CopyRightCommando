//Script:       EnemyManager
//Author:       Steven Motz
//Date:         9/20/2022
//Purpose:      This script is used for testing bullets.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1);
    }
    public float damage;
    private void OnTriggerEnter(Collider collision)
    {
        // if(!(collision.gameObject.tag == "Enemy"))
        /// Destroy(gameObject);
        PHealth player = collision.transform.GetComponent<PHealth>();

        if(player != null)
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
            
    }
    public float Damage()
    {
        Destroy(gameObject, .1f);
        return damage;
    }
}
