using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public LayerMask interactableLayer;
    Interactable interactable;
    public Image interactImage;
    public Sprite defaultIcon;
    public Sprite defaultInteraction;
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
              if(interactable.interaction != null)
                {
                    interactImage.sprite = interactable.interaction;
                }
                else
                {
                    interactImage.sprite = defaultInteraction;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.onInteract.Invoke();
                }
            }
        }
        else
        {
            if(interactImage.sprite != defaultIcon)
            {
                interactImage.sprite = defaultIcon;
                //Debug.Log("default");
            }
        }
    }
}
