//Just a test script can be deleted

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTest : MonoBehaviour
{
    public PHealth player;

    public void HurtPlayer()
    {
        player.TakeDamage(5);
        Debug.Log("IsWorking");
    }
}
