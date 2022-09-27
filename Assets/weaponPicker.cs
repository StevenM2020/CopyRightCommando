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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
        }
    }
}
