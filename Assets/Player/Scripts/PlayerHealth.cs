using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 50f;
    public float HitPoints { get { return hitPoints; } } // needed for health bar
    

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
