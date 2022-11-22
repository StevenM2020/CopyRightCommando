using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabDoorDetection : MonoBehaviour
{
    public GameObject wall;
    public GameObject boss;
    private bool blnActive = false;
    private void OnTriggerEnter(Collider collision)
    {
        if (!blnActive && collision.name == "Plaer")
        {
        wall.SetActive(true);
        boss.SetActive(true);
            blnActive = true;
            Debug.Log("did the thing");
        }

    }

}
