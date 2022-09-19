using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{   

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
    
}
