//Script:       EnemyManager
//Author:       Steven Motz
//Date:         9/20/2022
//Purpose:      This script shares information between the enemies.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void alertEnemies(int floor)
    {
        Debug.Log("player got seen");
        if (GameObject.FindGameObjectsWithTag("Enemy") != null)
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if(enemy.GetComponent<EnemyAI>().floor == floor)
                enemy.GetComponent<EnemyAI>().attackMode();
            }
    }
}
