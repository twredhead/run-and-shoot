using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    PlayerAttack playerAttack;
    Canvas escapeMenu;

    bool menuVisible = false;

    void Start()
    {
        escapeMenu = gameObject.GetComponent<Canvas>();

        escapeMenu.enabled = false; // hide the escape menu on start.

        playerAttack = FindObjectOfType<PlayerAttack>(); // this needs to be disabled on escape
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

            playerAttack.enabled = false;
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

            playerAttack.enabled = true;
        }

    }

    public void ButtonDisableEscape()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        escapeMenu.enabled = false;

        menuVisible = false;

        Time.timeScale = 1; // unpause the game

        playerAttack.enabled = true;
    }
}
