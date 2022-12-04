using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
    }
}
