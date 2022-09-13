using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 10f;

    float deathWaitTime = 1f;
    EnemyAI enemyAI;

    public void DamageTaken(float hitPointsDamage)
    {

        hitPoints -= hitPointsDamage;

        if ( enemyAI.IsPatrolling == false) { return; }

        enemyAI.SetIsPatrolling(false);       

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
            // in a larger project this should be changed to deactivate the enemy. Not destroy it.
            Destroy(gameObject, deathWaitTime); 

            // todo: add a funny vfx for enemy death
        }
    }
}
