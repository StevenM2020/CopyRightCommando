using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToHub : MonoBehaviour
{
   public void LoadHUB()
    {
        SceneManager.LoadScene("Hub");
    }
     void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            LoadHUB();
        }
    }
}
