using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Elevator : MonoBehaviour
{
    public Image img;
    public string sceneName;
    public bool enterElevator = false;
    public GameObject bell;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (enterElevator)
        {
            animator.SetBool("ElevatorClosed", true);
            StartCoroutine(FadeIn());

        }
        else
        {
            animator.SetBool("ElevatorOpen", true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ElevatorExit()
    {

        if (!enterElevator)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
            }

            StartCoroutine(FadeOut());
        }

    }
    IEnumerator FadeOut()
    {
        animator.SetBool("ElevatorClosing", true);
        Color c = img.color;
        c.a = 0;
        yield return new WaitForSeconds(2f);
        for (float alpha = 0f; alpha <= 1.1; alpha += 0.1f)
        {
            c.a = alpha;
            img.color = c;
            yield return new WaitForSeconds(.05f);
        }
        Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
        //GameManager.instance.AddToEnenmies(GameObject.FindGameObjectsWithTag("Enemy").Length);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeIn()
    {
        bell.SetActive(true);
        Color c = img.color;
        c.a = 1;
        img.color = c;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            img.color = c;
            yield return new WaitForSeconds(.05f);
        }
        animator.SetBool("ElevatorOpening", true);
    }
}
