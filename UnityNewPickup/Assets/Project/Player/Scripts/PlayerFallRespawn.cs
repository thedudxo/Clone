using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallRespawn : MonoBehaviour {

    private Vector3 spawnpoint;

    private void Start()
    {
        spawnpoint = gameObject.transform.position;   
    }

    void Update () {
	    if (gameObject.transform.position.y <= -200)
	    {
            gameObject.transform.position = spawnpoint;
	    }

    }
}
