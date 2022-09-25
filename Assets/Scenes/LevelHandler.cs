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
    }

    void Update()
    {
        levelTimer += Time.deltaTime;

        if(levelTimer <= 200)
        {
            blnTimeStar = true;
        }
        else
        {
            blnTimeStar = false;
        }

        if(GameObject.FindGameObjectsWithTag("Enemy") == null)
        {
                blnEnemyStar = true;
        }
 
        if(GameObject.FindGameObjectWithTag("Boss") == null)
        {
            blnBossStar = true;
        }
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
