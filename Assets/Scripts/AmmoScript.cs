using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    public int maxAmmo = 20,
                totalAmmo = 20,
                clipSize = 10,
                clip = 10;
    private void Start()
    {
        totalAmmo = maxAmmo;
        clip = clipSize;
    }
    public void Reload()
    {
        totalAmmo = maxAmmo;
    }
}