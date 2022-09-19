using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    
    Canvas startCanvas;
    Canvas playerHud;

    PlayerAttack playerAttack;
    PlayerMovementControls movementControls;
    MouseLook mouseLook;
    

    void Awake() 
    {
        startCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        Time.timeScale = 0; // time stops at the beginning of the game

        playerHud = GameObject.FindGameObjectWithTag("playerhud").GetComponent<Canvas>();

        playerHud.enabled = false;

        DisablePlayerControls();

    }

    void DisablePlayerControls()
    {   
        
        playerAttack = FindObjectOfType<PlayerAttack>();

        playerAttack.enabled = false;

        movementControls = FindObjectOfType<PlayerMovementControls>();

        movementControls.enabled = false;

        mouseLook = FindObjectOfType<MouseLook>();

        mouseLook.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.anyKeyDown )
        {
            startCanvas.enabled = false;

            EnablePlayerControls();

            Time.timeScale = 1;
        }
    }

    void EnablePlayerControls()
    {
        playerHud.enabled = true;
        playerAttack.enabled = true;
        movementControls.enabled = true;
        mouseLook.enabled = true;
    }
}
