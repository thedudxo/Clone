using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCubes : MonoBehaviour {
    public GameObject playerRespawnPos;

    public void OnTriggerEnter (Collider other)
    {

        if (other.tag == "Cube" && !other.GetComponent<Cloneable>().isClone)
        {
            other.GetComponent<CubeSpawnDeets>().Respawn();
        }
        if (other.tag == "Player")
        {
            other.transform.position = playerRespawnPos.transform.position;
        }

    }
}
