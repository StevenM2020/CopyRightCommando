//Script:       EnemyManager
//Author:       Steven Motz
//Date:         9/20/2022
//Purpose:      This script shares information between the enemies.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int floors = 3;
    private float stopAttackTime = 10;
    private float[] countDown;
    private bool[] enemiesAttacking;
    // Start is called before the first frame update
    void Start()
    {
        countDown = new float[floors];
        enemiesAttacking = new bool[floors];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < floors; i++)
        {
            if (enemiesAttacking[i])
                if (countDown[i] > 0)
                {
                    countDown[i] -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("enemies stop attacking");
                    enemiesAttacking[i] = false;
                    if (GameObject.FindGameObjectsWithTag("Enemy") != null)
                        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                        {
                            if (enemy.GetComponent<EnemyAI>().floor == i)
                                enemy.GetComponent<EnemyAI>().normalMode();
                        }
                }
        }
    }

    // tells all enemies on the same floor to attack and sets a the timer
    public void alertEnemies(int floor)
    {
        Debug.Log(floor);
        if (!enemiesAttacking[floor])
        {
            enemiesAttacking[floor] = true;
            Debug.Log("player got seen");
            if (GameObject.FindGameObjectsWithTag("Enemy") != null)
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy.GetComponent<EnemyAI>().floor == floor)
                        enemy.GetComponent<EnemyAI>().attackMode();
                }
        }
        countDown[floor] = stopAttackTime;
    }
}