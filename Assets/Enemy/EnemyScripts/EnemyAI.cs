using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{   
    // flag to turn on, or off patrolling. If enemy is aware of the player, is patrolling becomes false
    [SerializeField] bool isPatrolling = true; // turn this off if you want a sentry.

    Waypoint[] waypoints;
    NavMeshAgent navigation;

    // destination waypoint
    Transform destination;
    Transform lastDestination;

    float minSqrDistance = 1f;

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
        Patrol();
    }

    void Patrol()
    {   
        // do not patrol if false
        if ( isPatrolling == false ) { return; }
        
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

}
