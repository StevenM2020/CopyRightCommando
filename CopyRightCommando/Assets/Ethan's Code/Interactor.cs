using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interactor : MonoBehaviour
{
    public LayerMask interactableLayer;
    public Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2, interactableLayer))
        {
            if(hit.collider.GetComponent<Interactable>() != false)
            {
              if(interactable == null || interactable.ID != hit.collider.GetComponent<Interactable>().ID)
                {
                    interactable = hit.collider.GetComponent<Interactable>();
                    
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.onInteract.Invoke();
                }
            }
        }
    }
}
