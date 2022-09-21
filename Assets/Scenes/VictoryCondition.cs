using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
    int numberEnemies;
  
    Canvas victoryCanvas;
    LevelManager levelManager;


    public void ReduceEnemyCount()
    {
        numberEnemies--;
    }

    // Start is called before the first frame update
    void Start()
    {
        numberEnemies = transform.childCount;  
        
        victoryCanvas = GameObject.FindGameObjectWithTag("victory").GetComponent<Canvas>(); 

        victoryCanvas.enabled = false;

        levelManager = FindObjectOfType<LevelManager>();
    }



    void Update() 
    {
        if ( numberEnemies < 1 )
        {
            StartCoroutine(PlayerWins());
        }

    }

    IEnumerator PlayerWins()
    {
        

        levelManager.DisablePlayerControls();

        yield return new WaitForSeconds(1);
        
        victoryCanvas.enabled = true;

        Time.timeScale = 0;

        
    }
}
