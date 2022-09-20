using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GunData gunData;

    float timeSinceLastShot;
    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot()
    {
        Debug.Log("Shot Gun!");
    }


}
