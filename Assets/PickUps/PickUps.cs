using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUps : MonoBehaviour
{
    [SerializeField] bool isHealth;
    [SerializeField] bool isAmmo;
    [SerializeField] float healthAmount = 10f;
    [SerializeField] int ammoAmount = 15;

    PlayerHealth playerHealth;
    PlayerAttack playerAttack;
    Material pickUpColour;

    void Awake() 
    {
        pickUpColour = GetComponent<Renderer>().material;
        ColourFromType();
    }

    void Start() 
    {
        playerHealth = FindObjectOfType<PlayerHealth>();   
        playerAttack = FindObjectOfType<PlayerAttack>(); 
    }

    void ColourFromType()
    {

        if ( isHealth == true && isAmmo == false )
        {
            pickUpColour.color = Color.green;
        }

        else if ( isAmmo == true && isHealth == false )
        {
            pickUpColour.color = Color.red;
        }

        else if ( isAmmo == true && isHealth == true )
        {
            pickUpColour.color = Color.Lerp(Color.red, Color.green, 0.5f);
        }

    }

void OnTriggerEnter(Collider other) 
{
    if ( other.tag != "player" ) { return; }

    if ( isHealth == true && isAmmo == false )
    {
        playerHealth.IncreaseHitPoints( healthAmount );
        
        Destroy(gameObject);
    }

    else if ( isAmmo == true && isHealth == false )
    {
        playerAttack.IncreaseAmmo(ammoAmount);
        
        Destroy(gameObject);
    }

    else if ( isAmmo == true && isHealth == true )
    {
        
        playerHealth.IncreaseHitPoints( healthAmount );
        
        playerAttack.IncreaseAmmo(ammoAmount);
        
        Destroy(gameObject);

    }
}

}
