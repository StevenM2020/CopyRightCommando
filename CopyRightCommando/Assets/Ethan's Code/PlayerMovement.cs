////Ethan Lehutsky
///This script handles the basic movement of the player using rigidbodies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float playerHeight = 2f;
    public Transform orientation;
    Crouch crouchScript;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;
    float airMultiplier = 0.4f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed;
    public float sprintSpeed = 1.5f;
    public bool isSprinting;

    [Header("Jump")]
    public float jumpForce = 5f;

    [Header("Drag")]
    public float groundDrag = 6f;
    public float airDrag = 0f;
    bool gravCheck;

    [Header("Keykinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.4f;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    public Rigidbody rb;

    RaycastHit slopeHit;

    //Checks if the player is on a slope and returns a slopeMovement value
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        walkSpeed = moveSpeed;
        crouchScript = GetComponent<Crouch>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        SpeedControl();
        Sprint();

        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKey(sprintKey))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(sprintKey))
        {
            isSprinting = false;
        }


        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        if (!crouchScript.isSliding)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
       
    }

    //Adds drag to the player to stop sliding feeling
    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else if (!isGrounded)
        {
           rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    //Adds force to player based on moveDirection and changes based on the players current ground
    void MovePlayer()
    {
        if(isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    void Sprint()
    {
        if (isSprinting)
        {
            moveSpeed = walkSpeed * sprintSpeed;
        }
        else if (!isSprinting)
        {
            moveSpeed = walkSpeed;
        }
    }

    //Limits max velocity a player can reach
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limits velocity
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
       
    }
}
