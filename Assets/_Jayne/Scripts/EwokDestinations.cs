using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EwokDestinations : MonoBehaviour
{
    [SerializeField]
    List<DrawGizmos> cubeDropPoints;
    public int startWayPointIndex;
    public static bool goCarryCube = false;
    public bool thisAgentCarriesCubes;
    public int dropWayPointIndex;
    public GameObject cubeToCarry;
    public float pickupProximity = 0.8f;
    public GameObject CubeHolder;

    int currentWayPointIndex;
    NavMeshAgent navmeshAgent;
    Transform cubePickupPoint;
    Transform thisAgentTransform;
    Collider cubeCollider;
    Rigidbody cubeRigidBody;
    bool isCarrying = false;
    bool assignedToCube = false;
    string cubeName;
    void Start()
    {
        navmeshAgent = gameObject.GetComponent<NavMeshAgent>();
        if (thisAgentCarriesCubes)
        {
            cubePickupPoint = cubeToCarry.transform;
            thisAgentTransform = gameObject.transform;
            cubeCollider = cubeToCarry.GetComponent<Collider>();
            cubeRigidBody = cubeToCarry.GetComponent<Rigidbody>();
            cubeName = cubeToCarry.name;
        }
        #region 
        if (navmeshAgent == null) // This is only needed during development, not for the build
        {
            Debug.LogError("NavmeshAgent component is not attached to " + gameObject.name);
        }
        else
        {
            if (cubeDropPoints != null || startWayPointIndex <= cubeDropPoints.Count)
            {
                currentWayPointIndex = startWayPointIndex;
                SetDestination(currentWayPointIndex);
            }
            else
            {
                Debug.LogError("Either StartWayPoint is outside the List, or there is no Waypoint attached to " + gameObject.name);
            }
        }
        #endregion
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        //            if (one of the target cubes sets off the fallRespawn collider OnTriggerEnter function )
        {
            goCarryCube = true;
        }
        //if carrying a Cube, when close to drop point, drop it
        if (thisAgentCarriesCubes && isCarrying)
        {
            CarryCube();
            if (navmeshAgent.remainingDistance <= pickupProximity)
            {
                DropCube();
            }
        }
        //        if told to pick up a cube, go to it and when close, pick it up
        if (goCarryCube && thisAgentCarriesCubes && !isCarrying)
        {
            TargetCube();
            if (navmeshAgent.remainingDistance <= pickupProximity)
            {
                GrabCube();
            }
        }

        //if not picking up cubes or carrying them, wander around randomly
        if (!thisAgentCarriesCubes || (!goCarryCube && !isCarrying))
            if (navmeshAgent.remainingDistance <= pickupProximity)
            {
                int newWaypointIndex = UnityEngine.Random.Range(1, cubeDropPoints.Count);
                SetDestination(newWaypointIndex);
            }
    }

    public void TargetCube() // Face and go to Cube 
    {
        cubePickupPoint = cubeToCarry.transform;
        thisAgentTransform.LookAt(cubePickupPoint);
        Vector3 targetCubeVector = cubePickupPoint.position;
        navmeshAgent.SetDestination(targetCubeVector);
    }

    private void GrabCube() //
    {
        cubeCollider.enabled = true;
        cubeRigidBody.detectCollisions = true;
        isCarrying = true;
        GoToAltar();
    }

    private void CarryCube()
    {
        cubeToCarry.transform.position = CubeHolder.transform.position;
        goCarryCube = false;
    }


    private void GoToAltar()
    {
        SetDestination(dropWayPointIndex);
    }

    public void DropCube() // Drop Cube at Altar
    {
        cubeCollider.enabled = true;
        cubeRigidBody.detectCollisions = true;
        isCarrying = false;
    }

    public void SetDestination(int waypointIndex)
    {
  //      Debug.Log(" SetDestination called on waypoint " + waypointIndex + " for Ewok " + gameObject.name);
        if (cubeDropPoints != null) //If the List isn't empty
        {
            if (waypointIndex >= cubeDropPoints.Count) //If the proposed destination is outside the List, make it inside the List
            {
                Debug.Log("waypointIndex is outside the range" + waypointIndex);
                waypointIndex = cubeDropPoints.Count - 1;
            }
            Vector3 targetVector = cubeDropPoints[waypointIndex].transform.position;
            navmeshAgent.SetDestination(targetVector);
        }
    }

}

