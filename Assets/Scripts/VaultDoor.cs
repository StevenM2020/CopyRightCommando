using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : MonoBehaviour
{
 public void openVault()
    {
        GetComponent<Animator>().enabled = true;
    }
}
