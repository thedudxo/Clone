using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EwokDestinations : MonoBehaviour
{
    [SerializeField]
    List<DrawGizmos> ewokPoints;
    public int startWayPointIndex;
    public bool goPickupCube = false;
    public bool thisEwokPicksUpCubes;
    public int dropWayPointIndex;
    public GameObject targetCube;
    public float pickupProximity = 0.8f;
    public GameObject CubeHolder;

    int currentWayPointIndex;
    NavMeshAgent navmeshAgent;
    Transform targetCubePos;
    Transform thisEwokTransform;
    Collider cubeCollider;
    Rigidbody cubeRigidBody;
    bool isCarrying = false;
    bool assignedToCube = false;

    void Start()
    {
        navmeshAgent = gameObject.GetComponent<NavMeshAgent>();
        if (thisEwokPicksUpCubes)
        {
            targetCubePos = targetCube.transform;
            thisEwokTransform = gameObject.transform;
            cubeCollider = targetCube.GetComponent<Collider>();
            cubeRigidBody = targetCube.GetComponent<Rigidbody>();
        }
        #region 
        if (navmeshAgent == null) // This is only needed during development, not for the build
        {
            Debug.LogError("NavmeshAgent component is not attached to " + gameObject.name);
        }
        else
        {
            if (ewokPoints != null || startWayPointIndex <= ewokPoints.Count)
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
        {
            goPickupCube = true;
        }
        //if carrying a Cube, when close to Altar, drop it
        if (thisEwokPicksUpCubes && isCarrying)
        {
            CarryCube();
            if (navmeshAgent.remainingDistance <= pickupProximity)
            {
                DropCube();
            }
        }
        //        if told to pick up a cube, go to it and when close, pick it up
        if (goPickupCube && thisEwokPicksUpCubes && !isCarrying)
        {
            TargetCube();
            if (navmeshAgent.remainingDistance <= pickupProximity)
            {
                GrabCube();
            }
        }
        //if not picking up cubes or carruing them, wander around randomly
        if (!thisEwokPicksUpCubes || (!goPickupCube && !isCarrying))
            if (navmeshAgent.remainingDistance <= pickupProximity)
            {
                int newWaypointIndex = UnityEngine.Random.Range(0, ewokPoints.Count);
                SetDestination(newWaypointIndex);
            }
    }

    public void TargetCube() // Face and go to Cube 
    {
        targetCubePos = targetCube.transform;
        thisEwokTransform.LookAt(targetCubePos);
        Vector3 targetCubeVector = targetCubePos.position;
        navmeshAgent.SetDestination(targetCubeVector);
    }

    private void GrabCube() //
    {
        cubeCollider.enabled = true;
        cubeRigidBody.detectCollisions = true;
        isCarrying = true;
        Debug.Log("isCarrying is " + isCarrying);
        GoToAltar();
    }

    private void CarryCube()
    {
        targetCube.transform.position = CubeHolder.transform.position;
        goPickupCube = false;
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
        if (ewokPoints != null) //If the List isn't empty
        {
            if (waypointIndex >= ewokPoints.Count) //If the proposed destination is outside the List, make it inside the List
            {
                Debug.Log("waypointIndex is outside the range" + waypointIndex);
                waypointIndex = ewokPoints.Count - 1;
            }
            Vector3 targetVector = ewokPoints[waypointIndex].transform.position;
            navmeshAgent.SetDestination(targetVector);
        }
    }

}

