using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PHealth player;
    int w1, w2;
    private int enemies = 0;
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //weapon2.SetActive(false);
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
        if (GameObject.Find("Player"))
        {
            //if (weapon1 == null || weapon2 == null)
            //{
            //    weapon1 = GameObject.Find(weaponName1);
            //    weapon2 = GameObject.Find(weaponName2);
            //    weapon2.SetActive(false);
            //}
            if (player == null)
                player = GameObject.Find("Player").GetComponent<PHealth>();
        }
        if(player.currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
    public void ResetEnemyCount()
    {
        enemies = 0;
    }
    public void AddToEnenmies(int count)
    {
        enemies += count;
    }
    public int GetEnemies()
    {
        return enemies;
    }
    public int GetWeapon1()
    {
        return w1;
    }
    public int GetWeapon2()
    {
        return w2;
    }
    public void SetWeapon1(int weapon1)
    {
        w1 = weapon1;
    }
    public void SetWeapon2(int weapon2)
    {
        w2 = weapon2;
    }
}
