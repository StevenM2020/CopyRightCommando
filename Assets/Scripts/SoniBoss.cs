using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SoniBoss : MonoBehaviour
{
    public float health = 300;
    public PHealth PlayerHealth;
    Rigidbody testBullet;
    public GameObject bullet;
    private GameObject player;
    public float bulletSpeed =40;
    public GameObject spawnPoint;
    int attackTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "TestBullet")
        {

        }
        if (collision.gameObject.name == "Player")
        {
            PlayerHealth.TakeDamage(5);
        }

    }
    public void Damage(float damage)
    {
        health -= (int)damage;
        if (health <= 0)
        {
            Debug.Log("dead enemy");
            SceneManager.LoadScene("Win Screen");
            //send to win screne
        }
    }

    // Update is called once per frame
    void Update()
    {
        attackTime += 1;
        transform.LookAt(player.transform);
        if (attackTime % 50 == 0)
        {
            Debug.Log("throw bag");
            GameObject newBag = Instantiate(bullet);
            newBag.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y - 2, spawnPoint.transform.position.z);
            newBag.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z));
            newBag.GetComponent<Rigidbody>().velocity = newBag.transform.forward * bulletSpeed;
        }
        }
   
}
