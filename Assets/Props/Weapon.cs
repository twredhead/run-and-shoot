using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    
    [SerializeField] float range = 50f;
    [SerializeField] float hitPointsDamage = 5f;
    [SerializeField] Transform shooter;
    [SerializeField] ParticleSystem gunShotVFX;
    [SerializeField] GameObject enemyHitVFX;

    RaycastHit hit;
    EnemyHealth enemyHealth;

    public void FireWeapon()
    {
        if ( Physics.Raycast(shooter.transform.position, shooter.transform.forward, out hit, range) )
        {   
            Debug.Log($"object hit: {hit.transform.name}");
            WeaponVFX();
            DealDamage();
        }
    }

    void DealDamage()
    {
        if ( hit.transform.tag == "enemy")
        {   
            enemyHealth = hit.transform.GetComponent<EnemyHealth>();

            if ( enemyHealth == null ) { return; } // protect against crash

            enemyHealth.DamageTaken(hitPointsDamage);
        }
        else if ( hit.transform.tag == "player" )
        {
            // damage player health
        }
    }

    void WeaponVFX()
    {
        
        FireWeaponVFX();

        if (hit.transform.tag != "enemy" && hit.transform.tag != "player") { return; }

        EnemyHitVFX();

        // todo: add a environment hit condition and add a vfx for it.

    }

    void FireWeaponVFX()
    {
        if (gunShotVFX == null) { return; }

        gunShotVFX.Play();
    }

    void EnemyHitVFX()
    {   
        if (enemyHitVFX == null) { return; }

        // instantiate the enemy hit vfx at the point of impact, and make sure it is oriented normal to the surface of the object hit.
        GameObject impact = Instantiate(enemyHitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1); // get rid of the effect after 1 second
    }


}
