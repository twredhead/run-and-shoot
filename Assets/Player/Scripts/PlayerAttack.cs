using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] int ammoAmount = 10;
    public int AmmoAmmount { get { return ammoAmount; } }

    Weapon weapon;

    float waitToShoot = .5f;

    bool canShoot = true;

    public void IncreaseAmmo(int amount)
    {
        ammoAmount += amount;
    }

    void Start()
    {

        weapon = GetComponentInChildren<Weapon>();

    }

    void Update()
    {   
        if (Input.GetMouseButtonDown(0) && canShoot == true && ammoAmount > 0) 
        {
            StartCoroutine(Shoot());
        }      
    }

    IEnumerator Shoot()
    {   
        canShoot = false;
        
        weapon.FireWeapon(transform.forward);

        ammoAmount--; // decrease ammo when weapon is fired.
        
        yield return new WaitForSeconds(waitToShoot);
        
        canShoot = true;
    }

}
