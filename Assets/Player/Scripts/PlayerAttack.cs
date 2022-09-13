using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Weapon weapon;

    void Start()
    {

        weapon = GetComponentInChildren<Weapon>();

    }

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.FireWeapon();
        }
    }
}
