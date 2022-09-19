using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    
    float waitTime;

    void Start() 
    {
        waitTime = GetComponent<ParticleSystem>().main.duration;

        Destroy(gameObject, waitTime);
            
    }

}
