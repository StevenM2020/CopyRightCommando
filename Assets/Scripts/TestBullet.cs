//Script:       EnemyManager
//Author:       Steven Motz
//Date:         9/20/2022
//Purpose:      This script is used for testing bullets.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public bool blnShotByPlayer = false;
    public GameObject hitMarker;
    public GameObject audPlayer, audWall;
    private void Start()
    {
        Destroy(gameObject, 1);


    }
    public float damage;
    private void OnTriggerEnter(Collider collision)
    {
        // if(!(collision.gameObject.tag == "Enemy"))
        /// Destroy(gameObject);
        /// 
        if (blnShotByPlayer)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyAI>().Damage(damage);
                if (blnShotByPlayer)
                {
                    GameObject hit = Instantiate(hitMarker);
                    hit.transform.parent = GameObject.Find("InteractImage").transform;
                    hit.transform.position = GameObject.Find("InteractImage").transform.position;
                    hit.GetComponent<DestroySelf>().DestroyObject(.2f);
                }
            }else if(collision.name == "BlueSuitFree01")
            {
                collision.gameObject.GetComponent<Boss1>().Damage(damage);
                if (blnShotByPlayer)
                {
                    GameObject hit = Instantiate(hitMarker);
                    hit.transform.parent = GameObject.Find("InteractImage").transform;
                    hit.transform.position = GameObject.Find("InteractImage").transform.position;
                    hit.GetComponent<DestroySelf>().DestroyObject(.2f);
                }
            }
            else if (collision.name != "Player")
            {
                GameObject aud = Instantiate(audWall);
                aud.transform.position = collision.transform.position;
                aud.GetComponent<DestroySelf>().DestroyObject(1);
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.name == "Player")
            {
                PHealth player = collision.transform.GetComponent<PHealth>();

                if (player != null)
                {
                    player.TakeDamage(damage);
                    GameObject aud = Instantiate(audPlayer);
                    aud.transform.position = collision.transform.position;
                    aud.GetComponent<DestroySelf>().DestroyObject(1);
                }
            }
            Destroy(gameObject);
        }
        
            
    }
    public float Damage()
    {
        Destroy(gameObject, .01f);
        return damage;
    }
}
