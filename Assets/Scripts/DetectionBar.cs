using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBar : MonoBehaviour
{
    public GameObject bisneyBoss;
    //public float damage = 20;
    private void OnTriggerEnter(Collider other)
    {
       bisneyBoss.GetComponent<BisneyBoss>().BarHitPlayer();
        Debug.Log("playe hit with bar");
      // gameObject.SetActive(false);
    }
}
