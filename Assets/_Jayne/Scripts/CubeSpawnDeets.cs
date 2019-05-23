using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CubeSpawnDeets : MonoBehaviour {

    public GameObject respawnSpot;
    public NavMeshAgent deliveryAgent;

    //change these levels depending on floor levels of the final game
 /*   float l1FloorY = -0.5f;
    float l2FloorY = 11.0f;
    float l3FloorY = 18.0f;
    int LCubeIsOn;
*/
    [HideInInspector]
    public Vector3 deliverPos;
    [HideInInspector]
    public Vector3 spawnPos;

        
    // Use this for initialization
    void Start ()
    {
        deliverPos = gameObject.transform.position;
        spawnPos = respawnSpot.GetComponent<Transform>().position;
/*
        if (deliverPos.y < l1FloorY-1.0f)
        {
            LCubeIsOn = 1;
            Debug.Log("Cube is below Level 1");
        }
        if (deliverPos.y >= (l1FloorY - 1.0f) && deliverPos.y < (l2FloorY - 1.0f))
        {
            LCubeIsOn = 1;
            Debug.Log("Cube is on Level 1");
        }
        if (deliverPos.y >= (l2FloorY - 1.0f) && deliverPos.y < (l3FloorY - 1.0f))
        {
            LCubeIsOn = 2;
            Debug.Log("Cube is on Level 2");
        }

        if (deliverPos.y > l3FloorY-1.0f)
        {
            LCubeIsOn = 2;
            Debug.Log("Cube is above Level 2");
        }
*/
    }

    public void Respawn()
    {
        gameObject.transform.position = spawnPos;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        deliveryAgent.GetComponent<CubeDeliveryAgents>().goDeliverCube = true;
        deliveryAgent.GetComponent<CubeDeliveryAgents>().cubeToCarry = gameObject;
    }
}
