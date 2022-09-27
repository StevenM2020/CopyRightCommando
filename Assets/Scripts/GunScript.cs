//Script:       GunScript
//Author:       Steven Motz
//Date:         9/27/2022
//Purpose:      This script controls the players guns.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;

    public float damage;
    public int accuracyOffSet;
    public float shootDelay;
    public int numOfBullets = 10;

    private float tmrShoot;
    private float fltBulletSpeed = 100;
    private int numOfBulletsLeft;
    //private GameObject ammoText;
    public TextMeshProUGUI ammoText;
    System.Random rnd = new System.Random();
    private bool blnReloading = false;
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        numOfBulletsLeft = numOfBullets;
        ammoText = GameObject.Find("ammo").GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!blnReloading)
        {
            if (numOfBulletsLeft > 0)
            {
                if (tmrShoot <= 0)
                {
                    if (Input.GetMouseButton(0)) // shoot
                    {
                        GameObject newBullet = Instantiate(bullet, gameObject.transform.position, new Quaternion((float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100 + gameObject.transform.rotation.x, gameObject.transform.rotation.y + (float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100, gameObject.transform.rotation.z, gameObject.transform.rotation.w));
                        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * fltBulletSpeed;
                        newBullet.GetComponent<TestBullet>().damage = damage;
                        newBullet.GetComponent<TestBullet>().blnShotByPlayer = true;
                        numOfBulletsLeft--;
                    }
                    tmrShoot = shootDelay;
                }
                else
                {
                    tmrShoot -= Time.deltaTime;
                }
            }
            if (Input.GetKeyDown(KeyCode.R)) // reload
            {
                blnReloading = true;
                numOfBulletsLeft = numOfBullets;
                anim.Play();
                Invoke("reloaded", 2);
            }
            ammoText.text = "Bullets: " + numOfBulletsLeft;
        }
        else
        {
            ammoText.text = "Reloading";
        }
    }

    private void reloaded()
    {
        blnReloading = false;
    }

}
