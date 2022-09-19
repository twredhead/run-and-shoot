using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    
    Canvas gameOverCanvas;

    void Start() 
    {   
        // get the game over canvas and disable it
        gameOverCanvas = FindObjectOfType<GameOver>().GetComponent<Canvas>(); 
        gameOverCanvas.enabled = false;   
    }

    public void HandleDeath()
    {
        
        gameOverCanvas.enabled = true;

        Time.timeScale = 0; // stop time on death

        Cursor.visible = true; 

        Cursor.lockState = CursorLockMode.None;

    }
}
