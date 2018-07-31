using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Pickup : MonoBehaviour {

    public GameObject testCube;
    public GameObject pickupLocation;
    public GameObject RayStart;
    public float maxPickupDistance;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        Debug.DrawRay(RayStart.transform.position, RayStart.transform.forward * 100, Color.red);
        if (Input.GetKey(KeyCode.E))
        {
            Ray pickupRay = new Ray(RayStart.transform.position, RayStart.transform.forward);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(pickupRay, out hit))
            {
                GameObject lookingAt = hit.transform.gameObject;

                if (lookingAt.tag == "Grabbable" && hit.distance <= maxPickupDistance)
                {
                    lookingAt.transform.position = pickupLocation.transform.position;
                    lookingAt.transform.rotation = pickupLocation.transform.rotation;
                    lookingAt.GetComponent<Rigidbody>().isKinematic = true;
                }
                
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            testCube.GetComponent<Rigidbody>().isKinematic = false;
        }

	}

}
