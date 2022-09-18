using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    PlayerHealth playerHealth;
    float maxHitPoints; // get this from the PlayerHealth script

    void Start()
    {
        slider = GetComponent<Slider>();
        
        playerHealth = FindObjectOfType<PlayerHealth>();
        
        // if no player, don't crash
        if ( playerHealth == null ) { return; }

        maxHitPoints = playerHealth.HitPoints;

        // set the slider min and max values
        slider.maxValue = maxHitPoints;
        slider.minValue = 0;

    }

    void Update() 
    {

        CurrentHealth(playerHealth.HitPoints);

    }

    void CurrentHealth( float hitPoints )
    {
        slider.value = hitPoints;
    }
}
