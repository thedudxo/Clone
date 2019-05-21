using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCubes : MonoBehaviour {

    public GameObject L1YesCube_RespawnPoint;
    public GameObject L1NoCube_RespawnPoint;
    public GameObject L2YesCube_RespawnPoint;
    public GameObject L2NoCube_RespawnPoint;
    public GameObject L1_Player_RespawnPoint;
 //   public GameObject L2_Player_RespawnPoint;

    public void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Yes Cube")
        {
            other.transform.position = L2YesCube_RespawnPoint.transform.position;
            EwokDestinations.goDeliverCube = true;
        }
        if (other.tag == "NoCube")
        {
            other.transform.position = L2NoCube_RespawnPoint.transform.position;
            EwokDestinations.goDeliverCube = true;
        }
        if (other.tag == "L1YesCube")
        {
            other.transform.position = L1YesCube_RespawnPoint.transform.position;
            EwokDestinations.goDeliverCube = true;
        }
        if (other.tag == "L1NoCube")
        {
            other.transform.position = L1NoCube_RespawnPoint.transform.position;
            EwokDestinations.goDeliverCube = true;
        }
        if (other.tag == "Player")
        {
            other.transform.position = L1_Player_RespawnPoint.transform.position;
        }

    }
}
