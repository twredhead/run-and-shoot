using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    int numberEnemies;
    bool isVictory = false;
    public bool IsVictory { get { return isVictory; } }

    public void ReduceEnemyCount()
    {
        numberEnemies--;
    }

    // Start is called before the first frame update
    void Start()
    {
        numberEnemies = transform.childCount;   
    }

    void Update() 
    {
        if ( numberEnemies > 0 ) { return; }

        isVictory = true;

    }

}
