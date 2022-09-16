using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 10f;

    float deathWaitTime = 1f;
    EnemyAI enemyAI;

    public void EnemyDamageTaken(float hitPointsDamage)
    {

        hitPoints -= hitPointsDamage;

        enemyAI.SetIsPatrolling(false);   
        enemyAI.SetIsAlerted(true);    
        
    }

    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
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

            // in a larger project this should be changed to deactivate the enemy. Not destroy it.
            Destroy(gameObject, deathWaitTime); 

            // todo: add a funny vfx for enemy death
        }
    }
}
