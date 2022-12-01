using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pistol : MonoBehaviour
{
    public GameObject bullet;
    public GameObject[] spawnPoint;
    public float damage;
    public int accuracyOffSet;
    public float shootDelay;
    public float reloadDelay = 2;
    //public int numOfBullets = 10;
    //public int totalBullets = 20;
    //public int maxBullets = 20;

    private float tmrShoot;
    private float fltBulletSpeed = 10;
    //private int numOfBulletsLeft;
    //private GameObject ammoText;
    public TextMeshProUGUI ammoText;
    System.Random rnd = new System.Random();
    private bool blnReloading = false;
    private Animator animator;

    Ammo ammo;
    // Start is called before the first frame update
    void Start()
    {
        ammoText = GameObject.Find("ammo").GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
        ammo = GetComponent<Ammo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ammo.totalAmmo + ammo.clip == 0)
        {
            ammoText.text = "no ammo";
            return;
        }
        if (!blnReloading)
        {
            if (ammo.clip > 0)
            {
                if (tmrShoot <= 0)
                {
                    if (Input.GetMouseButton(0)) // shoot
                    {
                        animator.SetTrigger("Fire");
                        foreach (GameObject point in spawnPoint)
                        {
                        GameObject newBullet = Instantiate(bullet, point.transform.position, new Quaternion((float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100 + gameObject.transform.rotation.x, gameObject.transform.rotation.y + (float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100, gameObject.transform.rotation.z, gameObject.transform.rotation.w));
                        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * fltBulletSpeed;
                        newBullet.GetComponent<PaperBallScript>().StartPaper(1, 5, gameObject.transform);
                        newBullet.GetComponent<PaperBallScript>().blnShotByPlayer = true;
                        }

                        ammo.clip--;
                        tmrShoot = shootDelay;
                    }
                    
                }
                else
                {
                    tmrShoot -= Time.deltaTime;
                }
            }
            if (Input.GetKeyDown(KeyCode.R) && ammo.clip < ammo.clipSize && ammo.totalAmmo > 0) // reload
            {
                blnReloading = true;
                if (ammo.totalAmmo - (ammo.clipSize - ammo.clip) > 0)
                {
                    ammo.totalAmmo -= ammo.clipSize - ammo.clip;
                    ammo.clip = ammo.clipSize;
                }
                else
                {
                    ammo.clip += ammo.totalAmmo;
                    ammo.totalAmmo = 0;
                }
                animator.SetTrigger("Reload");
                Invoke("reloaded", reloadDelay);
            }
            ammoText.text = "Ammo: " + ammo.totalAmmo + " / "+ ammo.clip;
        }
        else
        {
            ammoText.text = "Reloading";
        }
        if (Input.GetMouseButtonUp(0) && tmrShoot < shootDelay/2)
        {
            tmrShoot = 0;
        }
    }

    private void reloaded()
    {
        blnReloading = false;
    }

}