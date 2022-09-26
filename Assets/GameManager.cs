using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PHealth player;
    //public GameObject gun1, gun2;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PHealth>();
       // if(gun1.GetComponent<Gun>() == null)
       // {
      //      gun1.AddComponent<Gun>();
            //gun1.GetComponent<Gun>().gun = gun1;
       // }
       // if (gun2.GetComponent<Gun>() == null)
       // {
       //     gun2.AddComponent<Gun>();
        //}
    }

    private void Update()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponent<PHealth>();

        if(player.currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
