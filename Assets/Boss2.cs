using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public int boss2Health = 500;
    public PlayerHealthTest PlayerHealth;
    int attackPattern = Random.Range(1, 3);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "TestBullet")
        {
            boss2Health -= 5;
        }
        if (collision.gameObject.name == "Player")
        {
            PlayerHealth.HurtPlayer();
        }



        // Update is called once per frame
        void Update()
        {

            if (attackPattern == 1)
            {
                //This will be the Soni Mech Punch

            }
            else if (attackPattern == 2)
            {
                //This is the Slamming both fists on ground causing a shoke wave
            }
            else
            {
                //This is the Soni Mech Kick
            }
        }
    }
}
