using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBallScript : MonoBehaviour
{
    // public bool blnShotByPlayer = false;
    // public GameObject hitMarker;
    // public GameObject audPlayer, audWall;
    public float damage;
    private void Start()
    {
        


    }
    public void StatDestroy(float destroyDelay)
    {
        Destroy(gameObject, destroyDelay);
    }
    private void OnTriggerEnter(Collider collision)
    {
        // if(!(collision.gameObject.tag == "Enemy"))
        /// Destroy(gameObject);
        /// 

            if (collision.name == "Player")
            {
                PHealth player = collision.transform.GetComponent<PHealth>();

                if (player != null)
                {
                    player.TakeDamage(damage);
                    //GameObject aud = Instantiate(audPlayer);
                    //aud.transform.position = collision.transform.position;
                    //aud.GetComponent<DestroySelf>().DestroyObject(1);
                }
            }
            Destroy(gameObject);
    }
    public float Damage()
    {
        Destroy(gameObject, .01f);
        return damage;
    }

}
