using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{

    LevelManager levelManager;
    Canvas escapeMenu;

    bool menuVisible = false;

    void Start()
    {
        escapeMenu = gameObject.GetComponent<Canvas>();

        escapeMenu.enabled = false; // hide the escape menu on start.

        levelManager = FindObjectOfType<LevelManager>();

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

            levelManager.DisablePlayerControls();
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

            levelManager.EnablePlayerControls();
        }

    }

    public void ButtonDisableEscape()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        escapeMenu.enabled = false;

        menuVisible = false;

        Time.timeScale = 1; // unpause the game

        levelManager.EnablePlayerControls();
    }
}
