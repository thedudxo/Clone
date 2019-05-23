using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeDeliveryAgents : MonoBehaviour {

    public bool goDeliverCube = false;
    public GameObject CubeHolder;
    Vector3 spawnPos;
    public float pickupProximity = 1.0f;
    [HideInInspector]
    public Vector3 deliverPos;
    [HideInInspector]
    public GameObject cubeToCarry;
    public GameObject defaultDeliverGizmo;   //eg the Big Altar or for playtesting, in the room you are testing
    public GameObject defaultSpawnGizmo;    //eg for playtesting, on the navmesh just outside the room you are testing
    NavMeshAgent deliveryAgent;
    Transform thisAgentTransform;
    Collider cubeCollider;
    Rigidbody cubeRigidBody;
    bool isCarrying = false;
    string cubeName;

    void Start () {
        deliverPos = defaultDeliverGizmo.GetComponent<Transform>().position;
        spawnPos = defaultSpawnGizmo.GetComponent<Transform>().position; 
        deliveryAgent = GetComponent<NavMeshAgent>();
        thisAgentTransform = gameObject.transform;
    }

    void FixedUpdate ()
    {
        if (isCarrying)
        {
            CarryCube(cubeToCarry);
            if (deliveryAgent.remainingDistance <= pickupProximity)
            {
                DropCube(cubeToCarry);
            }
        }

        if (goDeliverCube && !isCarrying)
        {
            CollectDelivery(cubeToCarry);
            if (deliveryAgent.remainingDistance <= pickupProximity)
            {
                GrabCube(cubeToCarry);
            }
        }

    }

    public GameObject CollectDelivery(GameObject cubeToCarry) // Face and go to Cube 
    {
 //       Vector3 pos = cubeToCarry.GetComponent<CubeSpawnDeets>().spawnPos;
        Vector3 pos = cubeToCarry.GetComponent<Transform>().position;
        Debug.Log("arrived at CollectDelivery, Pos is " + pos);
        thisAgentTransform.LookAt(pos);
        Debug.Log("arrived at CollectDelivery, spawnPos is " + pos);
        deliveryAgent.SetDestination(pos);
        return cubeToCarry;
    }

    private GameObject GrabCube(GameObject cubeToCarry)
    {
        cubeCollider = cubeToCarry.GetComponent<Collider>();
        cubeRigidBody = cubeToCarry.GetComponent<Rigidbody>();
        cubeCollider.enabled = false;
        cubeRigidBody.detectCollisions = false;
        isCarrying = true;
        Deliver(cubeToCarry);
        return cubeToCarry;
    }

    private GameObject CarryCube(GameObject cubeToCarry)
    {
        cubeToCarry.transform.position = CubeHolder.transform.position;
        goDeliverCube = false;
        return cubeToCarry;
    }

    public void DropCube (GameObject cubeToCarry) // Drop Cube at Altar
    {
        cubeCollider.enabled = true;
        cubeRigidBody.detectCollisions = true;
        isCarrying = false;
    }

    public GameObject Deliver(GameObject CubeToCarry)
    {
        Vector3 deliverPos = cubeToCarry.GetComponent<CubeSpawnDeets>().deliverPos;
        deliveryAgent.SetDestination(deliverPos);
        return cubeToCarry;
    }






}
