using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;

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
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthBar2.SetHealth(currentHealth);
    }

    
}
