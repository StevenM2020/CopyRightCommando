using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("Speed")]
    PlayerMovement moveSpeed;
    float baseSpeed;
    float baseSlideSpeed;
    float testSpeed;
    float testSlide;

    [Header("Crouch/Sliding")]
    public Transform ceilingCheck;
    public float ceilingDistance;
    public LayerMask whatIsCeiling;
    public float reducedHeight;
    public float crouchSpeedMultiplier;
    public float slideSpeed;
    public float slideLimit;
    public float slideWaitTime;

    bool noUncrouch;
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

       
    }

    // Update is called once per frame
    void Update()
    {
        noUncrouch = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, whatIsCeiling);

        if(isSliding == true)
        {
            Sliding();
            StartCoroutine("SlideLimiter");
        }else if (isSliding == false)
        {
            StopCoroutine("SlideLimiter");
            GetUp();
        }

        
            
        MyInput();

       
      

    }

    void MyInput()
    {
        if(Input.GetKeyDown(crouchKey) && moveSpeed.isSprinting)
        {
            isSliding = true;
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            isSliding = false;
        }




    }

    private void Sliding()
    {

        if (isSliding)
        {
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

            testSpeed = moveSpeed.moveSpeed;
            testSlide = slideSpeed;

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
        if(!noUncrouch)
        {
            playerScale.localScale = new Vector3(originalX, originalHeight, originalZ);
        }
        else if (!noUncrouch && moveSpeed.isSprinting && isSliding)
        {
            moveSpeed.moveSpeed = baseSpeed;
            slideSpeed = baseSlideSpeed;
            Debug.Log("I am your problem");
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(ceilingCheck.position, ceilingDistance);
    }


}
