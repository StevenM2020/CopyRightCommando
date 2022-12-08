using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int maxAmmo = 20,
                totalAmmo = 20,
                clipSize = 10,
                clip = 10;
    public void Reload()
    {
        totalAmmo = maxAmmo;
    }
}