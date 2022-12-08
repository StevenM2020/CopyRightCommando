using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth = 100;

    public HealthBar healthBar;
    public HealthBar healthBar2;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar2.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("health1 " + currentHealth);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthBar2.SetHealth(currentHealth);
        Debug.Log("health2 " + currentHealth);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Lose Screen");
        }
    }
   

}
