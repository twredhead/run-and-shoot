using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Weapon weapon;

    float waitToShoot = .5f;

    bool canShoot = true;
    void Start()
    {

        weapon = GetComponentInChildren<Weapon>();

    }

    void Update()
    {   
        if (Input.GetMouseButtonDown(0) && canShoot == true ) 
        {
            StartCoroutine(Shoot());
        }      
    }

    IEnumerator Shoot()
    {   
        canShoot = false;
        
        weapon.FireWeapon();
        
        yield return new WaitForSeconds(waitToShoot);
        
        canShoot = true;
    }

}
