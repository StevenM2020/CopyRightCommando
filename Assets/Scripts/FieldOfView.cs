//Script:       FieldOfView
//Author:       Steven Motz
//Date:         9/20/2022
//Purpose:      This script looks for the player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public float radius;
    [Range(0f, 360f)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.Find("Player");
        StartCoroutine(FOVRoutine());
    }
    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck() // checks if the enemy can see the player in the default radius
    {
        try {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeePlayer = true;
                    }
                    else
                    {
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }

            }
            else if (canSeePlayer)
            {
                canSeePlayer = false;
            }
        }
        catch
        {
            canSeePlayer = false;
        }
    } 
    public bool IsFacingPlayer(float newRadius) // checks if the enemy can see the player in the custom radius
    {
        try
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, newRadius, targetMask);
            if (rangeChecks != null)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        return true;
                    }
                }
            }
        }
        catch
        {
            return false;
        }
        return false;
    }
    public bool IsFacingPlayer(float newRadius, float newAngle) // checks if the enemy can see the player in the custom radius and angle
    {
        try
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, newRadius, targetMask);
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < newAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    return true;
                }
            }
        }
        catch
        {
            return false;
        }
        return false;
    }
}
