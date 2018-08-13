using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Pickup : MonoBehaviour {

    private GameObject heldCube;
    public GameObject pickupLocation;
    public GameObject RayStart;
    public float maxPickupDistance = 3;

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

                if ((lookingAt.GetComponent<Grabbable>() != null) && hit.distance <= maxPickupDistance)
                {
                    lookingAt.transform.position = pickupLocation.transform.position;
                    lookingAt.transform.rotation = pickupLocation.transform.rotation;
                    lookingAt.GetComponent<Rigidbody>().isKinematic = true;
                    heldCube = lookingAt;
                }
                
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && heldCube != null)
        {
            heldCube.GetComponent<Rigidbody>().isKinematic = false;
            heldCube = null;
        }

	}

}
