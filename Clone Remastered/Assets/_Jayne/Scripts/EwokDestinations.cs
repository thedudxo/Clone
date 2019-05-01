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
            Debug.Log("navmeshAgent speed is " + navmeshAgent.speed);
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            goPickupCube = true;
        }
        //        PickupCube and take it to Altar
        if (goPickupCube && thisEwokPicksUpCubes && !assignedToCube)
        {
            TargetCube();          
        }
        if (assignedToCube && thisEwokPicksUpCubes && !isCarrying)
        {
            if (navmeshAgent.remainingDistance <= pickupProximity)
            {
                CarryCube();
            }
        }
  
        if (assignedToCube && thisEwokPicksUpCubes && isCarrying)
        {
            if (navmeshAgent.remainingDistance <= pickupProximity)
            {
                DropCube();
            }
        }

        else if(!thisEwokPicksUpCubes || (!goPickupCube && !isCarrying) )
        if (navmeshAgent.remainingDistance <= pickupProximity)
        {
            int newWaypointIndex = UnityEngine.Random.Range(0, ewokPoints.Count);
            SetDestination(newWaypointIndex);
        }
    }

    public void TargetCube() // Now assigned to go to Cube 
    {
        Debug.Log("Now assigned to go to Cube");
        targetCubePos = targetCube.transform;
        thisEwokTransform.LookAt(targetCubePos);
        Vector3 targetCubeVector = targetCubePos.position;
        navmeshAgent.SetDestination(targetCubeVector);
        goPickupCube = false;
        assignedToCube = true;
    }

    private void CarryCube()
    {
        Debug.Log("Now close enough to carry Cube");
        cubeCollider.enabled = false;
        cubeRigidBody.detectCollisions = false;
        gameObject.transform.SetParent(targetCube.transform.parent); // targetCube is now the child of the navmeshAgent                                                                           // targetCube.transform.parent = gameObject.transform;
        isCarrying = true;
        Debug.Log("Parenting; isCarrying is " + isCarrying);
        GoToAltar();
    }

    private void GoToAltar()
    {
        Vector3 targetDropVector = ewokPoints[dropWayPointIndex].transform.position;
        navmeshAgent.SetDestination(targetDropVector);
    }

    public void DropCube() // Drop Cube at Altar
    {
        Debug.Log("Now close enough to drop Cube");
        targetCube.transform.parent = null; // release the Cube (targetCube no longer child of the navmeshAgent)  
        cubeCollider.enabled = true;
        cubeRigidBody.detectCollisions = true;
        isCarrying = false;
        assignedToCube = false;
//        SetDestination(startWayPointIndex); // revert to random destinations until the pickupCube is again set to true    
    }
    
    public void SetDestination(int waypointIndex)
    {
        Debug.Log(" SetDestination called on waypoint " + waypointIndex + " for Ewok " + gameObject.name);
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

