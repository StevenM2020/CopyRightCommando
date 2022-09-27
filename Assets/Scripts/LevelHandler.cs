using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelHandler : MonoBehaviour
{
    float levelTimer;
    int level;
    bool blnTimeStar;
    bool blnEnemyStar;
    bool blnBossStar;
   
 


void Start()
    {
        level = 1;
       for(int i = 0; i<3; i++)
        {
            Debug.Log(GameObject.FindGameObjectsWithTag("Enemy")[i]);
        }
    }

    void Update()
    {
        levelTimer += Time.deltaTime;

        //if(levelTimer <= 200)
        //{
        //    blnTimeStar = true;
        // }
        // else
        // {
        //     blnTimeStar = false;
        // }

        //Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").GetLength(int i));

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
                blnEnemyStar = true;
            Debug.Log("One Star");
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log(enemies.Length);
        }
 
       // if(GameObject.FindGameObjectWithTag("Boss") == null)
      //  {
         //   blnBossStar = true;
        //}
        //This will determine what stars the user gets for the current level.
    }

    void WriteString()
    {
        string path = "Assets/Resources/LevelData.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.Write(level.ToString() + ",");
        writer.Write(blnTimeStar.ToString() + ",");
        writer.Write(blnEnemyStar.ToString() + ",");
        writer.WriteLine(blnTimeStar.ToString());
        writer.Close();
    //This saves the stars for the levels
    //Figure out how to overwrite level data for when players replay levels.
    //Look into Json Objects for a possible alt way of programming this, same with arrays.
    }
}
