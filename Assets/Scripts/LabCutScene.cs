using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LabCutScene : MonoBehaviour
{
    public string sceneName;
    public bool startFade;
    private bool startedFade;

    public Image img;
    // Start is called before the first frame update
    void Start()
    {
    
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if(!startedFade && startFade)
        {
            startedFade = true;
            StartCoroutine(FadeOut());
        }
    }


    IEnumerator FadeOut()
    {
        Color c = img.color;
        c.a = 0;
        yield return new WaitForSeconds(2f);
        for (float alpha = 0f; alpha <= 1.1; alpha += 0.1f)
        {
            c.a = alpha;
            img.color = c;
            yield return new WaitForSeconds(.05f);
        }
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeIn()
    {
        Color c = img.color;
        c.a = 1;
        img.color = c;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            img.color = c;
            yield return new WaitForSeconds(.05f);
        }
    }

}
