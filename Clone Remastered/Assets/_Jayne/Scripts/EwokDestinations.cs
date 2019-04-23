using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EwokDestinations : MonoBehaviour
{
    [SerializeField]
    List<DrawGizmos> ewokPoints;
    //   public GameObject buttonZone;
    //   public GameObject cloneableCube;
    public int startWayPointIndex;
    int currentWayPointIndex;
    NavMeshAgent navmeshAgent;

    // Use this for initialization
    void Start()
    {
        navmeshAgent = gameObject.GetComponent<NavMeshAgent>();
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
        if (navmeshAgent.remainingDistance <= 0.5f)
        {
            int newWaypointIndex = UnityEngine.Random.Range(0, ewokPoints.Count);
            SetDestination(newWaypointIndex);
        }
    }

    public void SetDestination(int waypointIndex)
    {
 //       Debug.Log(" SetDestination called on waypoint " + waypointIndex + " for Ewok " + gameObject.name);
        if (ewokPoints != null) //If the List isn't empty
        {
            if (waypointIndex >= ewokPoints.Count) //If the proposed destination is outside the List, make it inside the List
            {
                Debug.Log("waypointIndex is outside the range" + waypointIndex);
                waypointIndex = ewokPoints.Count - 1;
            }
            Vector3 targetVector = ewokPoints[waypointIndex].transform.position;
            //            Vector3 targetVector = buttonZone.transform.position;
            navmeshAgent.SetDestination(targetVector);
        }
    }
}

