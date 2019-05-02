using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallRespawn : MonoBehaviour {

    private Vector3 spawnpoint;

    private void Start()
    {
        setSpawnHere();
    }

    public void setSpawnHere()
    {
        spawnpoint = gameObject.transform.position;
    }

    public void setSpawnThere(Vector3 newspawn)
    {
        spawnpoint = newspawn;
    }

    void Update () {
	    if (gameObject.transform.position.y <= -200)
	    {
            gameObject.transform.position = spawnpoint;
	    }

    }
}
