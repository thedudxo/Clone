using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Clone : MonoBehaviour {

    public GameObject targetObject;   // The real object the player wants to clone
    public GameObject clonedObject;   // The current cloned object visible in the world
    public GameObject targetPosition; // Position to clone new objects at (in front of player)
    public GameObject emptyClone;    // An emptyGameobject used to reset the clone gun
    public GameObject RayStart;


    void Start () {
        clonedObject = emptyClone;
        //targetObject = emptyClone;
	}
	

	void Update () {

        if (Input.GetMouseButtonDown(1)) //right click
        {
            Debug.Log("object Copied");

            Ray cloneRay = new Ray(RayStart.transform.position, RayStart.transform.forward);
            RaycastHit hit = new RaycastHit();
        }


        if (Input.GetMouseButton(0)) //left click
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(clonedObject);
                clonedObject = Instantiate(targetObject);
                clonedObject.SetActive(false);
            }

            {
                clonedObject.transform.position = targetPosition.transform.position;
                clonedObject.transform.rotation = targetPosition.transform.rotation;
                clonedObject.GetComponent<Rigidbody>().isKinematic = true;
                clonedObject.GetComponent<Rigidbody>().isKinematic = false; //Resets the "velocity" caused by "falling"
                clonedObject.SetActive(true);
            }
        }

        
	}
}
