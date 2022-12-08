using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchScript : MonoBehaviour
{
    public GameObject watch;
    public GameObject arrow;
    weaponPicker weaponPicker;
    // Start is called before the first frame update
    void Start()
    {
        weaponPicker = GetComponent<weaponPicker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3)) // set weapon 1 as active
        {
            watch.SetActive(true);
            weaponPicker.DisableWeapons();
            arrow.SetActive(!arrow.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            watch.SetActive(false);
            arrow.SetActive(true);
        }
    }
}
