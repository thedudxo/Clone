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
    public GameObject defaultDeliverGizmo;   //eg the Big Altar (or for playtesting, the room you are testing)
    public GameObject defaultSpawnGizmo;    //where the navMeshAgent returns to wait, must be on the navmesh, out of player view 
    NavMeshAgent deliveryAgent;
    Transform thisAgentTransform;
    Collider cubeCollider;
    Rigidbody cubeRigidBody;
    bool isCarrying = false;
    string cubeName;

    void Start () {
        deliverPos = defaultDeliverGizmo.GetComponent<Transform>().position;
        spawnPos = defaultSpawnGizmo.GetComponent<Transform>().position; //where Agents return to wait
        deliveryAgent = GetComponent<NavMeshAgent>();
        thisAgentTransform = gameObject.transform;
    }

    void FixedUpdate ()
    {
        if (isCarrying) //first priority is to deliver a cube being carried
        {
            CarryCube();
            if (deliveryAgent.remainingDistance <= pickupProximity)
            {
                DropCube();
            }
        }

        if (goDeliverCube && !isCarrying) // go collect cube 
        {
            CollectDelivery();
            if (deliveryAgent.remainingDistance <= pickupProximity)
            {
                GrabCube();
            }
        }
    }

    public void CollectDelivery() // Face and go to Cube's position (usu its spawnPos) (must be on navmesh or this will crash the game)
    {
        Vector3 pos = cubeToCarry.GetComponent<Transform>().position; // 
        thisAgentTransform.LookAt(pos);
        deliveryAgent.SetDestination(pos);
    }

    private void GrabCube() // turn off collider and rigidBody collisions so Cube can go through walls
    {
        cubeCollider = cubeToCarry.GetComponent<Collider>();
        cubeRigidBody = cubeToCarry.GetComponent<Rigidbody>();
        cubeCollider.enabled = false;
        cubeRigidBody.detectCollisions = false;
        isCarrying = true;
        Deliver();
    }

    private void CarryCube() //parent the cube to a 'holder' just in front of the Agent 
    {
        cubeToCarry.transform.position = CubeHolder.transform.position;
        goDeliverCube = false;
    }

    public void DropCube () // Drop Cube at correct "Altar" location for player to get
    {
        cubeCollider.enabled = true;
        cubeRigidBody.detectCollisions = true;
        cubeRigidBody.useGravity = true;
        isCarrying = false;
        deliveryAgent.SetDestination(spawnPos);
    }

    public void Deliver() //take the Cube to its correct location for player to find (must be on navMesh)
    {
        Vector3 deliverPos = cubeToCarry.GetComponent<CubeSpawnDeets>().deliverPos;
        deliveryAgent.SetDestination(deliverPos);
    }






}
