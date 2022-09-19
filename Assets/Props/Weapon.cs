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
    PlayerHealth playerHealth;
    AudioSource gunShotSFX;

    void Awake() 
    {
        gunShotSFX = GetComponent<AudioSource>();    
    }

    public void FireWeapon(Vector3 direction)
    {   
        if ( Physics.Raycast(shooter.transform.position, direction, out hit, range) )
        {   
            WeaponFX();
            DealDamage();
        }
    }

    void DealDamage()
    {
        if ( hit.transform.tag == "enemy")
        {   
            enemyHealth = hit.transform.GetComponent<EnemyHealth>();

            if ( enemyHealth == null ) { return; } // protect against crash

            enemyHealth.EnemyDamageTaken(hitPointsDamage);
        }
        else if ( hit.transform.tag == "player" )
        {
            playerHealth = hit.transform.GetComponent<PlayerHealth>();

            if ( playerHealth == null ) { return; }

            playerHealth.PlayerDamageTaken(hitPointsDamage);
        }
    }

    void WeaponFX()
    {
        
        FireWeaponVFX();
        
        FireWeaponSFX();

        if (hit.transform.tag != "enemy" && hit.transform.tag != "player") { return; }

        EnemyHitVFX();

        // todo: add a environment hit condition and add a vfx for it.

    }

    void FireWeaponVFX()
    {
        if (gunShotVFX == null) { return; }

        gunShotVFX.Play();
        
    }

    void FireWeaponSFX()
    {
        gunShotSFX.PlayOneShot(gunShotSFX.clip,1f);
    }

    void EnemyHitVFX()
    {   
        if (enemyHitVFX == null) { return; }

        // instantiate the enemy hit vfx at the point of impact, and make sure it is oriented normal to the surface of the object hit.
        GameObject impact = Instantiate(enemyHitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        
        
        Destroy(impact, 1); // get rid of the effect after 1 second
    }


}
