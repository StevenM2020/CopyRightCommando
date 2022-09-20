//Script:       EnemyAI
//Author:       Steven Motz
//Date:         9/20/2022
//Purpose:      This script controls the enemies movements and attacks
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    // private GameObject enemyManager;
    private EnemyManager enemyManager;
    private FieldOfView fov;
    private GameObject player;


    public GameObject bullet;
    private float fltBulletSpeed = 100;
    public float shootDelay = .2f;
    private bool shot = false;
    public int accuracyOffSet = 10;
    public float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        fov = GetComponent<FieldOfView>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState != EnemyState.dead)
        {
            switch (enemyState)
            {
                case EnemyState.normal:
                    if (Vector3.Distance(transform.position, target) < 1)
                    {
                        IterateWaypointIndex();
                        UpdateDestination();
                    }
                    break;
                case EnemyState.attack:
                    if (Vector3.Distance(transform.position, target) < playerShootDistance && fov.IsFacingPlayer(playerShootDistance)) // start shooting if in range and line of sight
                    {
                        GetComponent<NavMeshAgent>().speed = 0;
                        if (!shot)
                            StartCoroutine(shoot());
                    }
                    else // move to player
                    {
                        GetComponent<NavMeshAgent>().speed = moveSpeed;
                    }
                    target = player.transform.position;
                    break;

            }
            if (fov.canSeePlayer && enemyState != EnemyState.attack)
            {
                enemyManager.alertEnemies(floor);
            }

            if (health <= 0)
            {
                enemyState = EnemyState.dead;
                //start death animation
                Destroy(gameObject, 5f);
            }
        }
    }
    
    public void attackMode() // set the enemy to attack state
    {
        target =player.transform.position;
        agent.SetDestination(target);
        enemyState = EnemyState.attack;
        Debug.Log("attack mode enabled");
    }
    
    void UpdateDestination()
    {
        target = waypoints[waypointindex].position;
        agent.SetDestination(target);
    }
    void IterateWaypointIndex()
    {
        waypointindex++; 
        if(waypointindex == waypoints.Length)
        {
            waypointindex = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "paper")
        {
            if (fov.IsFacingPlayer(100)){ // alert if looking at player
                enemyManager.alertEnemies(floor);
            }
            else // move to shot
            {
                enemyState = EnemyState.alert;
                target = player.transform.position;
            }

            health -= collision.gameObject.GetComponent<TestBullet>().Damage();
        }
    }

    private IEnumerator shoot() // fire a bullet where enemy is looking
    {
        System.Random rnd = new System.Random();
        GameObject newBullet = Instantiate(bullet, transform.position, new Quaternion((float)rnd.Next(-accuracyOffSet, accuracyOffSet)/100, transform.rotation.y, (float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 50, transform.rotation.w));
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * fltBulletSpeed;
        shot = true;
        yield return new WaitForSeconds(shootDelay);
        shot = false;
    }
}
