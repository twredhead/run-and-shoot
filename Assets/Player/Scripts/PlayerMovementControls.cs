using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if ( isAirborn == true ) { return; }
        
        // get player input
        float forwardBackward = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");

        // convert input to speed, Time.deltaTime ensures machine independent performance 
        float speed = RunSpeedOrWalkSpeed();

        Vector3 forceVector = new Vector3(strafe, 0, forwardBackward) * movementForce * Time.deltaTime;

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
