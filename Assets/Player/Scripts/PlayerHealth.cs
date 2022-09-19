using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 50f;
    
    float hitPoints;
    public float HitPoints { get { return hitPoints; } } // needed for health bar
    
    public void IncreaseHitPoints(float amount)
    {
        if ( hitPoints + amount <= maxHitPoints )
        {
            hitPoints += amount;
        }
        else
        {
            hitPoints = maxHitPoints;
        }
    }

    
    void Awake() 
    {
        hitPoints = maxHitPoints;
    }

    private void Update() 
    {
        PlayerDeath();    
    }

    public void PlayerDamageTaken(float hitPointsDamage)
    {
        hitPoints -= hitPointsDamage;
    }

    void PlayerDeath()
    {
        if ( hitPoints > 0 ) { return; }

        Debug.Log("I just died, lol");
    }
}
