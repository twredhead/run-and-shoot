using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{   
    Weapon weapon;

    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    public void Shoot()
    {
    
        weapon.FireWeapon();   
        
    }
}
