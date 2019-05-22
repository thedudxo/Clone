using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCubes : MonoBehaviour {

    public void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Cube" || other.tag == "Player")
        {
            Debug.Log("Cube has fallen " + other.name);
            other.GetComponent<CubeSpawnDeets>().Respawn();
        }
    }
}
