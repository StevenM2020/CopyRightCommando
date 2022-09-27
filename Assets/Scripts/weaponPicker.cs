//Script:       WeaponPicker
//Author:       Steven Motz
//Date:         9/27/2022
//Purpose:      This script switches the guns when the player presses the numbers.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPicker : MonoBehaviour
{
     GameObject weapon1, weapon2;
    
    // Start is called before the first frame update
    void Start()
    {
        weapon1 = GameManager.instance.weapon1;
        weapon2 = GameManager.instance.weapon2;
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon1 == null || weapon2 == null) // if the weapon is still null fix it
        {
            weapon1 = GameManager.instance.weapon1;
            weapon2 = GameManager.instance.weapon2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) // set weapon 1 as active
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))// set weapon 2 as active
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
        }
    }
}
