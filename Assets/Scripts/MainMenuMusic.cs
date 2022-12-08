using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioClip MainMenuSFX;

    public void Start()
    {
        gameObject.AddComponent<AudioSource>();
        GetComponent<AudioSource>().clip = MainMenuSFX;
        GetComponent<AudioSource>().Play();
    }
}
