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
    private int floor;
    private float playerShootDistance = 2;
    private float playerShootSpeed = 1;
    private float moveSpeed = 3.5f;

    // private GameObject enemyManager;
    private EnemyManager enemyManager;
    private FieldOfView fov;
    private GameObject player;
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
                if (Vector3.Distance(transform.position, target) < playerShootDistance)
                {
                    GetComponent<NavMeshAgent>().speed = 0;
                }
                else
                {
                    GetComponent<NavMeshAgent>().speed = moveSpeed;
                }
                target = player.transform.position;
                break;

        }
        if (fov.canSeePlayer && enemyState != EnemyState.attack)
        {
            enemyManager.alertEnemies();
        }
    }
    
    public void attackMode()
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
            if (fov.IsFacingPlayer(100)){
                enemyManager.alertEnemies();
            }
            else
            {
                enemyState = EnemyState.alert;
                target = player.transform.position;
            }
        }
    }
}
