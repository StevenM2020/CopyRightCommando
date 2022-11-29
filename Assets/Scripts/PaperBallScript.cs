using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBallScript : MonoBehaviour
{
    public GameObject[] paperBalls;
    GameObject ball;
    float damage = 10;    
    public bool blnShotByPlayer = false;
    public GameObject hitMarker;
    public GameObject audPlayer, audWall;
    public GameObject copyRightPaper;
    Transform startTrans;
    public int GetNumPaperBalls()
    {
        return paperBalls.Length;
    }
    public void StartPaper(int intBall, float newDamage, Transform newStartTrans)
    {
        ball = Instantiate(paperBalls[intBall], transform.position, transform.rotation);
        ball.transform.parent = transform;
        ball.transform.localScale= new Vector3 (30,30, 30);
        damage = newDamage;
        startTrans = newStartTrans;
    }


    private void Start()
    {
        Destroy(gameObject, 10);


    }
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
                    spawnPaper(collision.gameObject);
                    hit.GetComponent<DestroySelf>().DestroyObject(.2f);
                }
            }
            else if (collision.name == "BlueSuitFree01")
            {
                collision.gameObject.GetComponent<Boss1>().Damage(damage);
                if (blnShotByPlayer)
                {
                    GameObject hit = Instantiate(hitMarker);
                    hit.transform.parent = GameObject.Find("InteractImage").transform;
                    hit.transform.position = GameObject.Find("InteractImage").transform.position;
                    spawnPaper(collision.gameObject);
                    hit.GetComponent<DestroySelf>().DestroyObject(.2f);
                }
            }
            else if (collision.name != "Player")
            {
                GameObject aud = Instantiate(audWall);

                //aud.transform.position = collision.transform.position;
                //aud.GetComponent<DestroySelf>().DestroyObject(1);
                spawnPaper(collision.gameObject);
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
                    //aud.transform.position = collision.transform.position;
                    //aud.GetComponent<DestroySelf>().DestroyObject(1);
                    spawnPaper(collision.gameObject);
                }
            }
            Destroy(gameObject);
        }


    }
    void spawnPaper(GameObject parentObj)
    {
       GameObject paper = Instantiate(copyRightPaper, transform.position, transform.rotation);
        paper.transform.parent = parentObj.transform;
       paper.transform.LookAt(startTrans);
    }
    public float Damage()
    {
        Destroy(gameObject, .01f);
        return damage;
    }
}
