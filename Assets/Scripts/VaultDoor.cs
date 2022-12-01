using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : MonoBehaviour
{

   public AudioClip VaultOpenSFX;

 public void openVault()
    {
        GetComponent<Animator>().enabled = true;
        gameObject.AddComponent<AudioSource>();
        GetComponent<AudioSource>().clip = VaultOpenSFX;
        GetComponent<AudioSource>().Play();
    }
}
