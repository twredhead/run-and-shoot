using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthFill;
    Slider slider;
    PlayerHealth playerHealth;
    float maxHitPoints; // get this from the PlayerHealth script
    

    void Start()
    {
        
        slider = GetComponent<Slider>();

        playerHealth = FindObjectOfType<PlayerHealth>();

        healthFill.color = Color.green;

        // if no player, don't crash
        if (playerHealth == null) { return; }

        SetSliderMinAndMax();

    }

    void SetSliderMinAndMax()
    {
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

        if ( slider.value <= maxHitPoints / 2 )
        {
            healthFill.color = Color.yellow;
        }
        if ( slider.value <= maxHitPoints / 3 )
        {
            healthFill.color = Color.red;
        }
    }
}
