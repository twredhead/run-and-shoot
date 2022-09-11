using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementControls))]
public class MouseLook : MonoBehaviour
{
    [SerializeField] float horizontalLookSpeed = 550f;
    [SerializeField] float verticalLookSpeed = 700f;
    [SerializeField] float maxVerticalAngle = 75f;
    [SerializeField] Camera playerCamera;

    Transform cameraTransform;

    private void Awake() 
    {
        cameraTransform = playerCamera.transform;
    }

    void Start() 
    {   
        // hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // ensure that the camera is looking the same direction as the parent player object
        cameraTransform.Rotate(transform.localRotation.eulerAngles);
    }

    void Update() 
    {   
        HorizontalMouseControl();
        VerticalMouseControl();
    }

    void HorizontalMouseControl()
    {
        
        if ( Input.GetAxis("Mouse X") != 0 ) 
        {   
            float lookDirection = Input.GetAxis("Mouse X");

            transform.Rotate(0, lookDirection * horizontalLookSpeed * Time.deltaTime, 0);     
        }

    }

    void VerticalMouseControl()
    {   

        float lookDirection = Input.GetAxis("Mouse Y");

        // this angle is a condition to test against maxVerticalAngle. Player should not be able to
        // look past maxVerticalAngle relative to the player forward vector.
        float lookAngle = Vector3.Angle(transform.forward, cameraTransform.forward);

        // if the player is trying to look beyond the maxVerticalAngle, don't let them.
        if (cameraTransform.forward.y < 0 && lookAngle >= maxVerticalAngle && lookDirection < 0)
        {
            return;
        }
        else if (cameraTransform.forward.y > 0 && lookAngle >= maxVerticalAngle && lookDirection > 0)
        {
            return;
        }
        else
        {
            cameraTransform.Rotate(-lookDirection * verticalLookSpeed * Time.deltaTime, 0, 0);
        }             
        
    }


    void ShowMouseCursor()
    {   
        // when escape is hit, show the cursor.
        if ( Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
    }
}
