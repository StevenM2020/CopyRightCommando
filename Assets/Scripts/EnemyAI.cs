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
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
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
}
