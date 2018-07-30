using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Pickup : MonoBehaviour {

    public GameObject testCube;
    public GameObject pickupLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.E))
        {
            testCube.transform.position = pickupLocation.transform.position;
            testCube.transform.rotation = pickupLocation.transform.rotation;
            testCube.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            testCube.GetComponent<Rigidbody>().isKinematic = false;
        }

	}

}
