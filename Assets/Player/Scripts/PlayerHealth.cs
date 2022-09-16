using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 10f;

    private void Update() 
    {
        PlayerDeath();    
    }

    public void PlayerDamageTaken(float hitPointsDamage)
    {
        hitPoints -= hitPoints;

    }

    void PlayerDeath()
    {
        if ( hitPoints > 0 ) { return; }

        Debug.Log("I just died, lol");
    }
}
