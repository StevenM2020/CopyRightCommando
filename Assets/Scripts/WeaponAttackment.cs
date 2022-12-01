using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackment : MonoBehaviour
{
    public GameObject laser, light;

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && light != null)
        {
            light.SetActive(!light.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.G) && laser != null)
        {
            laser.SetActive(!laser.activeSelf);
        }
    }
}
