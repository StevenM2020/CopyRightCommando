//Script:       WorldBorder
//Author:       Steven Motz
//Date:         9/27/2022
//Purpose:      This script kills the player when they leave the game or any objects
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldBorder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            Destroy(other.gameObject);
    }
}
