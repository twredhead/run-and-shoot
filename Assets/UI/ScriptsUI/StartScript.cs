using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartScript : MonoBehaviour
{
    
    Canvas startCanvas;
    Canvas playerHud;
    LevelManager levelManager;
    

    void Awake() 
    {
        startCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        Time.timeScale = 0; // time stops at the beginning of the game

        playerHud = GameObject.FindGameObjectWithTag("playerhud").GetComponent<Canvas>();

        levelManager = FindObjectOfType<LevelManager>();

        playerHud.enabled = false;

        levelManager.DisablePlayerControls();

    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.anyKeyDown )
        {
            startCanvas.enabled = false;

            levelManager.EnablePlayerControls();

            playerHud.enabled = true;

            Time.timeScale = 1;

            Destroy(gameObject); // get rid of this canvas
        }
    }


}
