using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CubeSpawnDeets : MonoBehaviour {

    public GameObject respawnSpotL1;
    public GameObject respawnSpotL2;
    public NavmeshAgent Agent;
    //change these levels depending on floor levels of the final game
    float l1FloorY = -0.0f;
    public float l2FloorY = 11.0f;
    float l3FloorY = 18.0f;
    int LCubeIsOn;
    Vector3 deliverPos;
    Vector3 spawnPos;

        
    // Use this for initialization
    void Start () {
        deliverPos = gameObject.transform.position;
        
        if (deliverPos.y < l1FloorY-1.0f)
        {
            LCubeIsOn = 1;
            spawnPos = respawnSpotL1.GetComponent<Vector3>();
            Debug.Log("Cube is below Level 1");
        }
        if (deliverPos.y >= (l1FloorY - 1.0f) && deliverPos.y < (l2FloorY - 1.0f))
        {
            LCubeIsOn = 1;
            spawnPos = respawnSpotL1.GetComponent<Vector3>();

            Debug.Log("Cube is on Level 1");
        }
        if (deliverPos.y >= (l2FloorY - 1.0f) && deliverPos.y < (l3FloorY - 1.0f))
        {
            LCubeIsOn = 2;
            spawnPos = respawnSpotL2.GetComponent<Vector3>();
            Debug.Log("Cube is on Level 2");
        }

        if (deliverPos.y > l3FloorY-1.0f)
        {
            LCubeIsOn = 2;
            spawnPos = respawnSpotL2.GetComponent<Vector3>();
            Debug.Log("Cube is above Level 2");
        }

    }

    public void Respawn()
    {
        transform.position = spawnPos;
        CubeDeliveryAgents.goDeliverCube = true;
        CubeDeliveryAgents.CollectDelivery(spawnPos);        
    }


}
