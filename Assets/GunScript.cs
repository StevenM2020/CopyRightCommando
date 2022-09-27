using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bullet;

    public float damage;
    public int accuracyOffSet;
    public float shootDelay;

    private float tmrShoot;
    private float fltBulletSpeed = 100;
    System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tmrShoot <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                GameObject newBullet = Instantiate(bullet, gameObject.transform.position, new Quaternion((float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100 + gameObject.transform.rotation.x, gameObject.transform.rotation.y + (float)rnd.Next(-accuracyOffSet, accuracyOffSet) / 100, gameObject.transform.rotation.z, gameObject.transform.rotation.w));
                newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * fltBulletSpeed;
                newBullet.GetComponent<TestBullet>().damage = damage;
                newBullet.GetComponent<TestBullet>().blnShotByPlayer = true;
            }
            tmrShoot = shootDelay;
        }
        else
        {
            tmrShoot -= Time.deltaTime;
        }
    }
    

}
