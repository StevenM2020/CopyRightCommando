using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    float levelTimer;
    bool timeStar;
    bool enemyStar;
    bool bossStar;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;

        if(levelTimer <= 200)
        {
            timeStar = true;
        }
        else
        {
            timeStar = false;
        }
        //Checks the time, if the level was completed in under 2 minutes, player gets the time star.

        //Check to see if any enemy entities are still alive. If there are no enemies then enemyStar = true;

        //Check to see if boss is still alive. If the boss is no longer alive then bossStar = true;
    }
}
