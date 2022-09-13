using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    
    [SerializeField] float range = 50f;
    [SerializeField] Transform shooter;
    [SerializeField] ParticleSystem gunShotVFX;
    [SerializeField] GameObject enemyHitVFX;

    RaycastHit hit;

    public void FireWeapon()
    {
        if ( Physics.Raycast(transform.position, shooter.transform.forward, out hit, range) )
        {
            WeaponVFX();
        }
    }

    void WeaponVFX()
    {
        if (gunShotVFX == null) { return; }

        FireWeaponVFX();

        if (hit.transform.tag != "enemy" && hit.transform.tag != "player") { return; }

        EnemyHitVFX();

        // todo: add a environment hit condition and add a vfx for it.

    }

    void FireWeaponVFX()
    {
        gunShotVFX.Play();
    }

    void EnemyHitVFX()
    {   
        // instantiate the enemy hit vfx at the point of impact, and make sure it is oriented normal to the surface of the object hit.
        GameObject impact = Instantiate(enemyHitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1); // get rid of the effect after 1 second
    }
}
