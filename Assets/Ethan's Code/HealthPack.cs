using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    PHealth player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PHealth>();
    }

    public void Heal()
    {
        player.currentHealth = player.maxHealth;
        player.healthBar.SetHealth(player.currentHealth);
        player.healthBar2.SetHealth(player.currentHealth);
        Debug.Log("Heal");
        Destroy(this.gameObject);
    }
}
