using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Clone : MonoBehaviour {

    private GameObject targetObject;   // The real object the player wants to clone
    private GameObject clonedObject;   // The current cloned object visible in the world
    public GameObject targetPosition; // Position to clone new objects at (in front of player)
    public Material cloneMaterial;
    public GameObject RayStart;


    void Start () {
	}
	

	void Update () {

        if (Input.GetMouseButtonDown(1)) //right click
        {

            Ray cloneRay = new Ray(RayStart.transform.position, RayStart.transform.forward);
            RaycastHit hit = new RaycastHit();

            if(Physics.Raycast(cloneRay, out hit))
            {
                GameObject lookingAt = hit.transform.gameObject;

                if (lookingAt.GetComponent<Cloneable>() != null)
                {
                    targetObject = lookingAt;
                    Debug.Log("Object copied to clipboard");
                }
            }
        }


        if (Input.GetMouseButton(0) && targetObject != null) //left click
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(clonedObject);
                clonedObject = Instantiate(targetObject);
                //clonedObject.SetActive(false);
            }

            clonedObject.transform.position = targetPosition.transform.position;
            clonedObject.transform.rotation = targetPosition.transform.rotation;
            clonedObject.GetComponent<Rigidbody>().isKinematic = true;
            clonedObject.GetComponent<Rigidbody>().isKinematic = false; //Resets the "velocity" caused by "falling"
            clonedObject.SetActive(true);

            clonedObject.GetComponent<MeshRenderer>().material = cloneMaterial;
            
        }

        
	}
}
