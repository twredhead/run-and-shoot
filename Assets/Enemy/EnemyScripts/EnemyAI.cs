using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{   

    [SerializeField] EnemyAttack weapon;

    // flag to turn on, or off patrolling. If enemy is aware of the player, is patrolling becomes false
    [SerializeField] bool isPatrolling = true; // turn this off if you want a sentry.
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] int skill = 5;
    [SerializeField] Vector3 perturbation = new Vector3(1f, 0f, 0f );
    [SerializeField] float waitToShoot = 1f; // how long between shots

    NavMeshAgent navigation;
    GameObject target; // look to see this. If you can see it. Shoot at it.
    List<Waypoint> waypoints;
    [SerializeField] Vector3[] patrolPoints = new Vector3[2]; // holds the terminals of the enemy patrol
    


    RaycastHit objectInSight;
    Vector3 nullVector = new Vector3(0f,0f,0f);
    [SerializeField] Vector3 currentDestination; // patrol destination. Updated when reached
    float minDistanceToWaypoint = 0.1f;
    bool canSeeTarget = false; // switch to true if enemy can see player and back to false they no longer can
    bool isAlerted = false; // swithc to true when the enemy has seen the player, or been shot by the player
    bool canShoot = true; // if false enemy cannot shoot
    

    /*******************************************************************************************************************************/
    /***************************************************Public Methods**************************************************************/
    /*******************************************************************************************************************************/

    public void SetIsPatrolling(bool trueOrFalse)
    {
        // change is patrolling from outside this script.
        isPatrolling = trueOrFalse;
    }
    public void SetIsAlerted( bool trueOrFalse )
    {
        isAlerted = trueOrFalse;
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
        InitializePatrol();

        target = GameObject.FindGameObjectWithTag("player");

    }

    void Update()
    {   

        if ( isPatrolling == true )
        {
            Patrol();
        }

        LookForTarget();

        if ( canSeeTarget == true )
        {
            navigation.isStopped = true;

            // attack the player
            PointGunAtTarget();
            
            if ( canShoot == true )
            {
                StartCoroutine( Attack() );
            }
            
        }
        else if ( isAlerted == true && canSeeTarget == false)
        {
            navigation.isStopped = false;
            MoveToTarget();
        }


    }



    /*******************************************************************************************************************************/
    /***************************************************Navigation Methods**********************************************************/
    /*******************************************************************************************************************************/

    void InitializePatrol()
    {
        // set the two patrol points used for navigating the patrol route.
        FindPatrolPoints();

        currentDestination = patrolPoints[0]; // initialize the patrol destination
        navigation.SetDestination(currentDestination);
    }
    
    void Patrol()
    {

        if (navigation.remainingDistance < minDistanceToWaypoint)
        {   
            FindNextPatrolPoint();
            
            navigation.ResetPath();   
        }

        navigation.SetDestination(currentDestination);

    }


    void FindNextPatrolPoint()
    {   
        // find which patrol point was the last destination, then set the current destination to the vector
        // that was NOT the last destination
        foreach (Vector3 vector in patrolPoints)
        {
            if (vector != currentDestination)
            {   
                currentDestination = vector;

                return;
            }
        }
    }

    void FindPatrolPoints()
    {   
        // Only care about distances in xz plane
        Vector2 enemyPosition = new Vector2 ( transform.position.x, transform.position.z );

        waypoints = new List<Waypoint>(FindObjectsOfType<Waypoint>());

        Waypoint closestWaypoint = null;

        float closestWaypointDistance = Mathf.Infinity;
        
        // iterate through the index of patrolPoints (which is empty at the start)
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            // find the closest waypoint in waypoints.
            for (int j = 0; j < waypoints.Count; j++)
            {   
                Vector2 waypointPosition = new Vector2 ( waypoints[j].transform.position.x, waypoints[j].transform.position.z );

                // distance from waypoint i in list to enemy object.
                float distance = Vector3.SqrMagnitude(waypointPosition - enemyPosition);

                // if the current waypoint is closer than the last closest, this is the closest waypoint known so far
                if (distance < closestWaypointDistance)
                {

                    closestWaypointDistance = distance;

                    closestWaypoint = waypoints[j];

                }

            }

            // Having found the first patrol point, reset the closest waypoint distance to infinity
            closestWaypointDistance = Mathf.Infinity;

            // remove the closest waypoint from the list of waypoints, such that the next waypoint found is not 
            // the same waypoint.
            waypoints.Remove(closestWaypoint);

            patrolPoints[i] = closestWaypoint.transform.position;

        }
    }


    /*******************************************************************************************************************************/
    /***************************************************Find and Kill Player********************************************************/
    /*******************************************************************************************************************************/

    void LookForTarget()
    {

        if ( target == null ) { return; } // don't crash

        Vector3 searchDirection = DirectionToTarget(); // vector from position to target

        if (Physics.Raycast(transform.position, searchDirection, out objectInSight))
        {
            if (objectInSight.transform.name == target.transform.name)
            {
                canSeeTarget = true;
                isPatrolling = false;
                isAlerted = true;
            }
            else
            {
                canSeeTarget = false;

            }
        }

    }

    void MoveToTarget()
    {
        if ( target == null ) { return; }

        Vector3 location = target.transform.position;

        navigation.SetDestination(location); 
    }

    Vector3 DirectionToTarget()
    {
        if ( target == null ) { return new Vector3 (0,0,0); }

        return target.transform.position - transform.position;
    }

    void PointGunAtTarget()
    {
        
        RotateBodyTowardTarget();

        AimAtTarget();
        
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

    void AimAtTarget()
    {
        if ( target == null ) { return; }

        // skill based target direction
        Vector3 targetDirection = (target.transform.position - weapon.transform.position).normalized;

        Quaternion rotateTo = Quaternion.LookRotation( targetDirection );

        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, rotateTo, Time.deltaTime * rotationSpeed);

    }

    public Vector3 PerturbedTargetDirection()
    {
        if ( target == null ) { return new Vector3(0,0,0); }

        int hitChance = Random.Range(0,10);

        if (hitChance >= skill)
        {
            return (target.transform.position + perturbation - weapon.transform.position).normalized;
        }
        else
        {
            return (target.transform.position - weapon.transform.position).normalized;
        }
    }

    IEnumerator Attack()
    {   
        canShoot = false;

        weapon.Shoot();

        yield return new WaitForSeconds(waitToShoot);

        canShoot = true;
        
    }

}
