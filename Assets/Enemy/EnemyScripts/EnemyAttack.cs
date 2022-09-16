using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{   
    Weapon weapon;
    EnemyAI targetDirection;

    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        targetDirection = GetComponentInParent<EnemyAI>();
    }

    public void Shoot()
    {
    
        weapon.FireWeapon(targetDirection.PerturbedTargetDirection());   
        
    }
}
