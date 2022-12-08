using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubMusic : MonoBehaviour
{
    public AudioClip HubSFX;

    public void Start()
    {
        gameObject.AddComponent<AudioSource>();
        GetComponent<AudioSource>().clip = HubSFX;
        GetComponent<AudioSource>().Play();
    }
}
