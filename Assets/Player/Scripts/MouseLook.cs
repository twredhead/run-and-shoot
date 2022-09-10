using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        cameraTransform.Rotate(transform.localRotation.eulerAngles);
    }

    void Start() 
    {
        Cursor.visible = false;
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
  
        float lookAngle = Vector3.Angle(transform.forward, cameraTransform.forward);

        if (cameraTransform.forward.y < 0 && lookAngle > maxVerticalAngle && lookDirection < 0)
        {
            return;
        }
        else if (cameraTransform.forward.y > 0 && lookAngle > maxVerticalAngle && lookDirection > 0)
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
        if ( Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
  
        }
    }
}
