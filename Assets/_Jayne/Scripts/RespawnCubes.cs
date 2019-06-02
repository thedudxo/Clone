using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCubes : MonoBehaviour {
    public GameObject playerRespawnPos;

    public void OnTriggerEnter (Collider other)
    {

//If its a Yes Cube, but NOT already a Clone
        if (other.tag == "Cube" && other.GetComponent<Cloneable>() != null)
        {
            if (!other.GetComponent<Cloneable>().isClone)
            {
                other.GetComponent<CubeSpawnDeets>().Respawn();
            }
        }
//If its a No Cube
        if (other.tag == "Cube" && other.GetComponent<Cloneable>()==null)
        {
            other.GetComponent<CubeSpawnDeets>().Respawn();
        }

        if (other.tag == "Player")
        {
            other.transform.position = playerRespawnPos.transform.position;
        }
    }
}
