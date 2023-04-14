using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPanelFlicker : MonoBehaviour
{
    public GameObject light;
    public float minTime, maxTime;
    bool isFlickering = false;
    public Material matOn, matOff;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(Flicker());
        }
    }
    IEnumerator Flicker()
    {
        isFlickering = true;
        light.GetComponent<Light>().enabled = false;
        GetComponent<Renderer>().material = matOff;
        yield return new WaitForSeconds(UnityEngine.Random.Range(minTime, maxTime));
        light.GetComponent<Light>().enabled = true;
        GetComponent<Renderer>().material = matOn;
        yield return new WaitForSeconds(UnityEngine.Random.Range(minTime, maxTime));
        isFlickering = false;
    }

}
