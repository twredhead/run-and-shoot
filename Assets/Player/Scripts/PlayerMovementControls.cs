using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseLook))]
public class PlayerMovementControls : MonoBehaviour
{
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float movementForce = 1000f;

    bool isAirborn = false;
    float currentVelocity;

    Rigidbody rb;

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        rb.freezeRotation = true;
    }

    void OnCollisionEnter(Collision other) 
    {   
        // identifies that the player is no longer airborn.
        if( other.gameObject.tag == "surface")
        {
            isAirborn = false;
        }
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    void PlayerMove()
    {
        // cannot walk or run if not on a surface
        if ( isAirborn == true ) { return; }
        
        float forwardBackward = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");

        // speed is from player input
        float speed = RunSpeedOrWalkSpeed();

        // force vector for movement
        Vector3 forceVector = new Vector3(strafe, 0, forwardBackward) * movementForce * Time.deltaTime;

        // acceleration is proportional to force and we do not want constant acceleration.
        // To guard against this, movement force is only added if the magnitude of the velocity is less than 
        // the selected speed.
        if(rb.velocity.sqrMagnitude < speed*speed)
        {
            rb.AddRelativeForce(forceVector, ForceMode.Acceleration);
        }

    }

    float RunSpeedOrWalkSpeed()
    {
        // returns run speed based on player input
        if(Input.GetKey(KeyCode.LeftShift))
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }

    void PlayerJump()
    {
        // cannot jump if not on a surface
        if(isAirborn == true) { return; }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            isAirborn = true;
        }
        
    }

    
}
