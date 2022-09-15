using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{   

    [SerializeField] EnemyAttack weapon;
    [SerializeField] GameObject target; // look to see this. If you can see it. Shoot at it.

    // flag to turn on, or off patrolling. If enemy is aware of the player, is patrolling becomes false
    [SerializeField] bool isPatrolling = true; // turn this off if you want a sentry.
    [SerializeField] float rotationSpeed = 10f;

    Waypoint[] waypoints;
    NavMeshAgent navigation;

    // destination waypoint
    Transform destination;
    Transform lastDestination;

    RaycastHit objectInSight;

    float minSqrDistance = 1f;
    bool canSeeTarget = false; // switch to true if enemy can see player and back to false they no longer can
    bool canShoot = true; // if false enemy cannot shoot
    float waitToShoot = 0.5f; // how long between shots

    /*******************************************************************************************************************************/
    /***************************************************Public Methods**************************************************************/
    /*******************************************************************************************************************************/

    public void SetIsPatrolling(bool trueOrFalse)
    {
        // change is patrolling from outside this script.
        isPatrolling = trueOrFalse;
    }


    /*******************************************************************************************************************************/
    /***************************************************Awake, Start, Update********************************************************/
    /*******************************************************************************************************************************/    
    void Awake() 
    {
        navigation = GetComponent<NavMeshAgent>(); 
           
    }

    void Start() 
    {
        // find all the waypoints in the world
        waypoints = FindObjectsOfType<Waypoint>();
    }


    void Update()
    {   
        if ( isPatrolling == false)
        {
            navigation.isStopped = true;
        }
        else
        {
            Patrol();
        }

        LookForPlayer();

        if ( canSeeTarget == true )
        {
            // attack the player
            PointGunAtTarget();
            
            if ( canShoot == true )
            {
                StartCoroutine( Attack() );
            }
            
        }

    }



    /*******************************************************************************************************************************/
    /***************************************************Navigation Methods**********************************************************/
    /*******************************************************************************************************************************/

    void Patrol()
    {   

        FindWaypoint();

        // don't crash if no waypoint is found.
        if ( destination == null) { return; }

        // move toward the destination
        navigation.SetDestination(destination.position);

        // set lastDestination to destination shortly before arriving.
        if ( Vector3.SqrMagnitude( transform.position - destination.position) < minSqrDistance + 0.5 )
        {
            lastDestination = destination;
        }

    }

    void FindWaypoint()
    {   
        Transform waypoint;

        float nearestSqrDistance = Mathf.Infinity;

        // only care about distances in xz plane
        Vector2 position = new Vector2(transform.position.x, transform.position.z);
        
        // iterate through each waypoint in waypoints to find the closest waypoint
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoint = waypoints[i].transform;
            
            // location of waypoint
            Vector2 waypointPosition = new Vector3(waypoint.position.x, waypoint.position.z);

            float sqrDistance = Vector2.SqrMagnitude(position - waypointPosition);

            if ( sqrDistance < nearestSqrDistance && sqrDistance > minSqrDistance)
            {
                nearestSqrDistance = sqrDistance;

                // if we have arrived at a destination it will be the closest. We need a new destination.
                if ( waypoint != lastDestination)
                {
                    destination = waypoint;
                }
            }

        }
        
    }

    /*******************************************************************************************************************************/
    /***************************************************Find and Kill Player********************************************************/
    /*******************************************************************************************************************************/

    void LookForPlayer()
    {
        Vector3 searchDirection = DirectionToTarget(); // vector from position to target

        if (Physics.Raycast(transform.position, searchDirection, out objectInSight))
        {
            if (objectInSight.transform.name == target.transform.name)
            {
                canSeeTarget = true;
                isPatrolling = false;
            }
            else
            {
                canSeeTarget = false;

            }
        }

    }

    Vector3 DirectionToTarget()
    {
        return target.transform.position - transform.position;
    }

    void PointGunAtTarget()
    {
        
        RotateBodyTowardTarget();

        RotateGunTowardTarget();
        
    }

    void RotateBodyTowardTarget()
    {
        Vector3 targetDirection = DirectionToTarget().normalized;

        float xCoordinate = targetDirection.x;
        float zCoordinate = targetDirection.z; 

        // we only want to rotate the body in the horizontal plane. So keep y coordinate constant.
        Quaternion rotateTo = Quaternion.LookRotation( new Vector3( xCoordinate, transform.position.y, zCoordinate ) );

        transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, Time.deltaTime * rotationSpeed);

    }

    void RotateGunTowardTarget()
    {
        Vector3 targetDirection = (target.transform.position - weapon.transform.position).normalized;

        // We only want to rotate the gun in the yz plane 
        Quaternion rotateTo = Quaternion.LookRotation( targetDirection );

        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, rotateTo, Time.deltaTime * rotationSpeed);

    }

    IEnumerator Attack()
    {   
        canShoot = false;

        weapon.Shoot();

        yield return new WaitForSeconds(waitToShoot);

        canShoot = true;
        
    }

}
