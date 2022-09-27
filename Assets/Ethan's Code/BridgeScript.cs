using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
   public void SetActiveBool()
    {
        if(gameObject.activeInHierarchy == false)
        {
            gameObject.SetActive(true);
        }else if(gameObject.activeInHierarchy == true)
        {
            gameObject.SetActive(false);
        }
    }
}
