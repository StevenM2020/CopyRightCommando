//Boss 1 Script
// Developed by Andy Jackowski
//Controls everything relating to Boss 1, CEO of AE Sport
// Date Last Modified: 11/17/22


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss1 : MonoBehaviour
{
    public int boss1Health = 100;
    public PHealth PlayerHealth;
    //int attackPattern = Random.Range(1, 3);
    Rigidbody moneyBag;
    public GameObject bag;
    private GameObject player;
    int attackTime;
    private float fltBagSpeed = 40;
    public GameObject spawnPoint;
    
    private void Start()
    {
        player = GameObject.Find("Player");   
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "TestBullet")
        {
   
        }
        if(collision.gameObject.name == "Player")
        {
            PlayerHealth.TakeDamage(5);
        }
        if(collision.gameObject == null)
        {
            PlayerHealth.TakeDamage(5);
        }
            
    }
    public void Damage(float damage)
    {
        boss1Health -= (int)damage;
        if (boss1Health <= 0)
        {
            Debug.Log("dead enemy");
            SceneManager.LoadScene("Win Screen");
            //send to win screne
        }
    }
    private void Update()
    {
        attackTime += 1;
        transform.LookAt(player.transform);


       //if(attackPattern == 1)
       // {
       //     //This is where the boss will throw the money bags over head, like a catapult

       // }
       //else if(attackPattern == 2)
       // {
       //     //This is where the boss will throw the money bags like a curve ball, with the arm extented out and letting it go to curve
       // }
       // else
       // {
       //     //This is the bowling move, where the boss will roll the money bags.
       // }

       if(attackTime % 50 == 0)
        {
            Debug.Log("throw bag");
            GameObject newBag = Instantiate(bag);
            newBag.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y - 2, spawnPoint.transform.position.z);
            newBag.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z));
            newBag.GetComponent<Rigidbody>().velocity = newBag.transform.forward * fltBagSpeed;




            //Rigidbody clone;
            //clone = Instantiate(moneyBag, transform.position, transform.rotation);
            //clone.velocity = transform.TransformDirection(Vector3.forward * 10);
            //every 50 ticks it fires off a money bag

            
        }

    }



}
