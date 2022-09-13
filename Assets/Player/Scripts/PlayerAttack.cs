using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Weapon weapon;

    void Start()
    {

        weapon = GetComponentInChildren<Weapon>();

        Debug.Log($"My weapon is {weapon.name}");

    }

    void Update() 
    {
        if ( Input.GetMouseButtonDown(0) )
        {
            weapon.FireWeapon();
        }    
    }


}
