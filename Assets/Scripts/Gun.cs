using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public GunData gunData;
    [SerializeField] private Transform muzzle;
    float timeSinceLastShot;

    //public Gun gun = null;
    //private void Awake()
    //{
    //    Debug.Log("gun object is here");
    //    if (gun == null)
    //        gun = this;
    //    else if (gun != this)
    //        Destroy(gameObject);

    //    DontDestroyOnLoad(gameObject);
    //}


    private void Start()
    {
        Debug.Log("gun object is starting");
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        gunData.currentAmmoCount = gunData.magSize;
    }

    public void StartReload()
    {
            if (gameObject.active)
            if (!gunData.reloading)
            {
                StartCoroutine(Reload());
            }
        
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmoCount = gunData.magSize;
        gunData.reloading = false;
    }
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    private void Shoot()
    {
        //Debug.Log("Shot Gun!");
        if(gunData.currentAmmoCount > 0)
        {
           
            if(CanShoot())
            {
                Debug.Log("Shooting");
                if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    
                        EnemyAI enemy = hitInfo.transform.GetComponent<EnemyAI>();
                        if (enemy != null)
                        {
                            enemy.health -= gunData.damage;
                            Debug.Log("Enemy Hit! " + hitInfo.transform.name + " " + enemy.health);
                        }
                    
                    
                }

                gunData.currentAmmoCount--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
            
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward);

        Debug.Log(gunData);




    }

    private void OnGunShot()
    {

    }

}
