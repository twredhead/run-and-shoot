using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 10f;
    [SerializeField] GameObject enemyDeathVFX;

    EnemyAI enemyAI;
    VictoryCondition numberEnemies;
    GameObject vfxContainer;

    public void EnemyDamageTaken(float hitPointsDamage)
    {

        hitPoints -= hitPointsDamage;

        enemyAI.SetIsPatrolling(false);   
        enemyAI.SetIsAlerted(true);    
        
    }

    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();

        numberEnemies = FindObjectOfType<VictoryCondition>();

        vfxContainer = GameObject.FindGameObjectWithTag("vfxcontainer");

    }

    void Update() 
    {
        Death();    
    }

    void Death()
    {
        if ( hitPoints <= 0 )
        {   
            enemyAI.enabled = false; // don't let the enemy do things anymore

            GameObject funnyDeathVFX = Instantiate(enemyDeathVFX, transform.position, Quaternion.identity, vfxContainer.transform);

            numberEnemies.ReduceEnemyCount();

            // in a larger project this should be changed to deactivate the enemy. Not destroy it.
            Destroy(gameObject); 
  
        }
    }
}
