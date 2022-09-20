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
    private float playerShootDistance = 30;
    private float playerShootSpeed = 1;
    private float moveSpeed = 3.5f;

    // private GameObject enemyManager;
    private EnemyManager enemyManager;
    private FieldOfView fov;
    private GameObject player;


    public GameObject bullet;
    private float fltBulletSpeed = 100;
    private float shootDelay = .2f;
    private bool shot = false;
    private int accuracyOffSet = 10;
    
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
                if (Vector3.Distance(transform.position, target) < playerShootDistance && fov.IsFacingPlayer(playerShootDistance))
                {
                    GetComponent<NavMeshAgent>().speed = 0;
                    if (!shot)
                        StartCoroutine(shoot());
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
            enemyManager.alertEnemies(floor);
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
                enemyManager.alertEnemies(floor);
            }
            else
            {
                enemyState = EnemyState.alert;
                target = player.transform.position;
            }
        }
    }

    private IEnumerator shoot()
    {
        System.Random rnd = new System.Random();
        GameObject newBullet = Instantiate(bullet, transform.position, new Quaternion((float)rnd.Next(-accuracyOffSet, accuracyOffSet)/100, transform.rotation.y, (float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100, transform.rotation.w));
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * fltBulletSpeed;
        shot = true;
        yield return new WaitForSeconds(shootDelay);
        shot = false;
    }
}
