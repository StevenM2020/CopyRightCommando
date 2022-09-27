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
