using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   

    PlayerAttack playerAttack;
    PlayerMovementControls movementControls;
    MouseLook mouseLook;

    void Start() 
    {
        playerAttack = FindObjectOfType<PlayerAttack>();

        movementControls = FindObjectOfType<PlayerMovementControls>();

        mouseLook = FindObjectOfType<MouseLook>();

    }

    public void ReloadLevel()
    {
        
        // get the index of the current scene
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // reload the scene from the beginning

        SceneManager.LoadScene(sceneIndex);
    }
        
    public void QuitGame()
    {
        
        Application.Quit();

    }

    public void DisablePlayerControls()
    {   

        //if ( playerAttack == null || movementControls == null || mouseLook == null ) { return; }

        playerAttack.enabled = false;
        movementControls.enabled = false;
        mouseLook.enabled = false;

    }

    public void EnablePlayerControls()
    {
        //if ( playerAttack == null || movementControls == null || mouseLook == null ) { return; }

        playerAttack.enabled = true;
        movementControls.enabled = true;
        mouseLook.enabled = true;
    }




    
}
