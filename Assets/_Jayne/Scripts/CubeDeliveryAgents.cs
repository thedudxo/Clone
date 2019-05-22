using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDeliveryAgents : MonoBehaviour {

    public bool goDeliverCube = false;
    UnityEngine.AI.NavMeshAgent navmeshAgent;
    Transform spawnPos;
    Transform thisAgentTransform;
    Collider cubeCollider;
    Rigidbody cubeRigidBody;
    bool isCarrying = false;
    bool assignedToDeliver = false;
    string cubeName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (goDeliverCube && !isCarrying)
        {
            CollectDelivery();
            if (navmeshAgent.remainingDistance <= pickupProximity)
            {
                GrabCube();
            }
        }


    }

    public void TargetCube() // Face and go to Cube 
    {
        cubePickupPoint = spawnPos.transform;
        thisAgentTransform.LookAt(cubePickupPoint);
        Vector3 targetCubeVector = cubePickupPoint.position;
        navmeshAgent.SetDestination(targetCubeVector);
    }

    public void CollectDelivery(Vector3 spawnPos)
    {
        thisAgentTransform.LookAt(spawnPos);
        navmeshAgent.SetDestination(spawnPos);
    }

    private void Deliver(Vector3 deliverPos)
    {
        navmeshAgent.SetDestination(deliverPos);
    }



}
