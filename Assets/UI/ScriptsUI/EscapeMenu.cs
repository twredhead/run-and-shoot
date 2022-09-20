using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    PlayerAttack playerAttack;
    PlayerMovementControls movementControls;
    MouseLook mouseLook;
    Canvas escapeMenu;

    bool menuVisible = false;

    void Start()
    {
        escapeMenu = gameObject.GetComponent<Canvas>();

        escapeMenu.enabled = false; // hide the escape menu on start.

        playerAttack = FindObjectOfType<PlayerAttack>(); // this needs to be disabled on escape

        movementControls = FindObjectOfType<PlayerMovementControls>();

        mouseLook = FindObjectOfType<MouseLook>();
    }


    void DisablePlayerControls()
    {
        playerAttack.enabled = false;
        movementControls.enabled = false;
        mouseLook.enabled = false;
    }

    void EnablePlayerControls()
    {
        playerAttack.enabled = true;
        movementControls.enabled = true;
        mouseLook.enabled = true;
    }

    void Update()
    {
        if ( menuVisible == false )
        {
            EnableEscape();
        }
        else
        {
            DisableEscape();
        }

    }

    void EnableEscape()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            Time.timeScale = 0; // pause the game

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            escapeMenu.enabled = true;

            menuVisible = true;

            DisablePlayerControls();
        }
    }

    void DisableEscape()
    {
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            escapeMenu.enabled = false;

            menuVisible = false;

            Time.timeScale = 1; // unpause the game

            EnablePlayerControls();
        }

    }

    public void ButtonDisableEscape()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        escapeMenu.enabled = false;

        menuVisible = false;

        Time.timeScale = 1; // unpause the game

        EnablePlayerControls();
    }
}
