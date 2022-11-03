using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ElevatorEnter : MonoBehaviour
{
    public Image img;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToNextScene()
    {
        GameObject player = GameObject.Find("Player");
        if(player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        Color c = img.color;
        c.a = 0;
        yield return new WaitForSeconds(1f);
        for (float alpha = 0f; alpha <= 1.1; alpha += 0.1f)
        {
            c.a = alpha;
            img.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
