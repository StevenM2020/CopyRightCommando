//Script:       EnemyAI
//Author:       Steven Motz
//Date:         9/20/2022
//Purpose:      This script controls the enemies movements and attacks
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointindex;
    Vector3 target;
    enum EnemyState { normal, alert, attack, dead };
    EnemyState enemyState = EnemyState.normal;
    public int floor = 0;
    public float playerShootDistance = 30;
    public float playerShootSpeed = 1;
    private float moveSpeed = 3.5f;

    private EnemyManager enemyManager;
    private FieldOfView fov;
    private GameObject player;
    private float lookSpeed = 20;

    public GameObject bullet;
    public GameObject gun;
    public float damage;
    private float fltBulletSpeed = 100;
    public float shootDelay = .2f;
    private bool shot = false;
    public int accuracyOffSet = 10;
    public float health = 100;
    private float maxHealth;

    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        fov = GetComponent<FieldOfView>();
        player = GameObject.Find("Player");
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState != EnemyState.dead)
        {
            switch (enemyState)
            {
                case EnemyState.normal: // partrol to waypoints
                    if (Vector3.Distance(transform.position, target) < 1)
                    {
                        IterateWaypointIndex();
                        UpdateDestination();
                    }
                    break;
                case EnemyState.attack: // follow player and attack
                    if (Vector3.Distance(new Vector3(transform.position.x,0, transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z)) < playerShootDistance ) // start shooting if in range
                    {
                        if (fov.IsFacingPlayer(playerShootDistance)) // shoot if line of sight
                        {
                            agent.SetDestination(transform.position); // stop moveing

                            // rotate gun to player
                            Vector3 newDirection = player.transform.position - gun.transform.position;
                            Quaternion targetRotation = Quaternion.LookRotation(newDirection);
                            Quaternion lookAt = Quaternion.RotateTowards(gun.transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
                            gun.transform.rotation = lookAt;
                            enemyManager.alertEnemies(floor);
                            if (!shot) // shoot
                                StartCoroutine(shoot());
                        }
                        else
                        {
                            agent.SetDestination(player.transform.position); // move to player
                        }
                      
                        // rotate enemy to player
                            Vector3 newDirection1 = new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
                            Quaternion targetRotation1 = Quaternion.LookRotation(newDirection1);
                            Quaternion lookAt1 = Quaternion.RotateTowards(transform.rotation, targetRotation1, Time.deltaTime * lookSpeed);
                            transform.rotation = lookAt1;
                        
                    }
                    else // move to player
                    {
                        agent.SetDestination(player.transform.position);
                    }
                    break;
                case EnemyState.alert:
                   if (Vector3.Distance(transform.position, target) < 5)
                        enemyState = EnemyState.normal;
                    CancelInvoke("isAlert");
                    break;
            }
            if (fov.canSeePlayer) // alert to enemy manager if see player
            {
                enemyManager.alertEnemies(floor);
            }

            if (health <= 0) // die
            {
                enemyState = EnemyState.dead;
                //start death animation
                //Destroy(gameObject, 5f);
                Destroy(gameObject);
            }
        }
        healthBar.transform.LookAt(player.transform.position);
    }
    
    public void attackMode() // set the enemy to attack state
    {
        target =player.transform.position;
        agent.SetDestination(target);
        enemyState = EnemyState.attack;
        Debug.Log("attack mode enabled");
    }
    
    void UpdateDestination() // updates the agent with waypoint
    {
        target = waypoints[waypointindex].position;
        agent.SetDestination(target);
    }
    void IterateWaypointIndex() // change to next waypoint
    {
        waypointindex++; 
        if(waypointindex == waypoints.Length)
        {
            waypointindex = 0;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.transform.tag == "paper")
    //    {
    //        if (fov.IsFacingPlayer(100)){ // alert if looking at player
    //            enemyManager.alertEnemies(floor);
    //        }
    //        else // move to shot
    //        {
    //            enemyState = EnemyState.alert;
    //            target = player.transform.position;
    //        }

    //        health -= collision.gameObject.GetComponent<TestBullet>().Damage();
    //    }
    //}

    public void Damage(float bulletDamage)
    {


        //if (fov.IsFacingPlayer(100))
        //{ // alert if looking at player
        //    enemyManager.alertEnemies(floor);
        //}
        //else // move to shot
        //{
        //    enemyState = EnemyState.alert;
        //    target = new Vector3(player.transform.position.x, transform.position.y - 1, player.transform.position.z);
        //    Invoke("isAlert", 10);
        //Debug.Log("goToPlayer");
        //}
        enemyManager.alertEnemies(floor);
        health -= bulletDamage;
        healthBar.GetComponent<Slider>().value = health / maxHealth;
    }

    private IEnumerator shoot() // fire a bullet where enemy is looking
    {
        System.Random rnd = new System.Random();
        GameObject newBullet = Instantiate(bullet, gun.transform.position, new Quaternion((float)rnd.Next( -accuracyOffSet, accuracyOffSet)/100 + gun.transform.rotation.x, gun.transform.rotation.y + (float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100 , gun.transform.rotation.z, gun.transform.rotation.w));
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * fltBulletSpeed;
        newBullet.GetComponent<TestBullet>().damage = damage;
        shot = true;
        yield return new WaitForSeconds(shootDelay);
        shot = false;
    }
    public void normalMode() // back to patrolling
    {
        enemyState = EnemyState.normal;
        UpdateDestination();
    }
    public void DamageEnemy(float damage) 
    {
        health -= damage;
        healthBar.GetComponent<Slider>().value = health / maxHealth;
    }
    public void isAlert()
    {
        if(enemyState == EnemyState.alert)
        {
            enemyState = EnemyState.normal;
        }
    }
}
