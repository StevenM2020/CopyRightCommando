using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("Speed")]
    public float crouchSpeed;
    PlayerMovement moveSpeed;
    float baseSpeed;
    float baseSlideSpeed;
    float baseSprint;

    

    [Header("Crouch/Sliding")]
    public Transform ceilingCheck;
    public float ceilingDistance;
    public LayerMask whatIsCeiling;
    public float reducedHeight;
    public float crouchSpeedMultiplier;
    public float slideSpeed;
    public float slideLimit;
    public float slideWaitTime;

    public bool noUncrouch;
    bool isCrouching;
    public bool isSliding;
    Transform playerScale;
    float originalHeight;
    float originalX;
    float originalZ;

    [Header("KeyBinds")]
    public KeyCode crouchKey = KeyCode.LeftControl;
    
    
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = GetComponent<PlayerMovement>();
        baseSpeed = moveSpeed.moveSpeed;
        playerScale = GetComponent<Transform>();
        originalHeight = playerScale.localScale.y;
        originalX = playerScale.localScale.x;
        originalZ = playerScale.localScale.z;
        baseSlideSpeed = slideSpeed;
        baseSprint = moveSpeed.sprintSpeed;

       
    }

    // Update is called once per frame
    void Update()
    {
        noUncrouch = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, whatIsCeiling);

        if(isSliding == true && !isCrouching)
        {
            moveSpeed.isSprinting = false;
            Sliding();
            StartCoroutine("SlideLimiter");
        }else if (isSliding == false && !isCrouching)
        {
            StopCoroutine("SlideLimiter");
            GetUp();
        }else if(isCrouching && !isSliding)
        {
            Crouching();
        }

        
        
            
        MyInput();

       
      

    }

    void MyInput()
    {
        if(Input.GetKeyDown(crouchKey) && moveSpeed.isSprinting)
        {
            isSliding = true;
            
        }
        else if (Input.GetKeyDown(crouchKey) && !moveSpeed.isSprinting)
        {
           isCrouching = true;
            
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            isSliding = false;
            isCrouching = false;
        }




    }


    void Crouching()
    {
        if (isCrouching && !isSliding)
        {
            playerScale.localScale = new Vector3(originalX, reducedHeight, originalZ);
            moveSpeed.moveSpeed = crouchSpeed;
            Debug.Log("Crouch working");
        }
    }

    private void Sliding()
    {
        //Debug.Log("working");
        if (isSliding && !isCrouching)
        {
            Debug.Log("Slide working");
            playerScale.localScale = new Vector3(originalX, reducedHeight, originalZ);
            moveSpeed.rb.AddForce(moveSpeed.orientation.forward * slideSpeed, ForceMode.VelocityChange);
        }
    }

    IEnumerator SlideLimiter()
    {
        while (isSliding)
        {
            moveSpeed.moveSpeed = moveSpeed.moveSpeed - slideLimit;
            slideSpeed = slideSpeed - slideLimit;
            
            

            if(moveSpeed.moveSpeed < 0)
            {
                moveSpeed.moveSpeed = 0;
              
                moveSpeed.isSprinting = false;
            }

            if (slideSpeed < 0)
                slideSpeed = 0;

           

            yield return new WaitForSeconds(slideWaitTime);
        }
    }

    private void GetUp()
    {
        //Debug.Log("You Summoned me");
        if(!noUncrouch)
        {
            playerScale.localScale = new Vector3(originalX, originalHeight, originalZ);
            
        }
        else if (!noUncrouch)
        {
            Debug.Log("I am your problem");
            moveSpeed.moveSpeed = baseSpeed;
            slideSpeed = baseSlideSpeed;
            //moveSpeed.sprintSpeed = baseSprint;
           
        }
        else if (noUncrouch)
        {
            Debug.Log("Slow down");
            moveSpeed.moveSpeed = crouchSpeed;
            moveSpeed.isSprinting = false;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(ceilingCheck.position, ceilingDistance);
    }


}
