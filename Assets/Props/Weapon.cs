using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    
    [SerializeField] float range = 50f;

    [SerializeField] Transform shooter;

    RaycastHit hit;

    void Start() 
    {
        Debug.Log($"My shooter is {shooter}");    
    }

    public void FireWeapon()
    {
        if ( Physics.Raycast(transform.position, shooter.transform.forward, out hit, range) )
        {
            Debug.Log($"I hit {hit.transform.name}");
        }
    }
}
