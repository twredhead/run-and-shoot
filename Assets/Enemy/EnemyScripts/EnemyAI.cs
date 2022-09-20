using System.Collections;
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

    GameObject target; // look to see this. If you can see it. Shoot at it.
    Waypoint[] waypoints;
    NavMeshAgent navigation;

    // destination waypoint
    Transform destination;
    Transform lastDestination;

    RaycastHit objectInSight;

    float minSqrDistance = 1f;
    bool canSeeTarget = false; // switch to true if enemy can see player and back to false they no longer can
    bool isAlerted = false;
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
        // find all the waypoints in the world
        waypoints = FindObjectsOfType<Waypoint>();
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
