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
        //Connect with Ethan on what level the user selects, deppending on the level they select that will be the int level variable.
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
        writer.WriteLine(level.ToString());
        writer.WriteLine(blnTimeStar.ToString());
        writer.WriteLine(blnEnemyStar.ToString());
        writer.WriteLine(blnTimeStar.ToString());
        writer.Close();
    //This saves the stars for the levels
    }
}
