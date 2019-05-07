using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamButton : MonoBehaviour {

    public int cubes = 0;
    [SerializeField] Material greenArrow;
    [SerializeField] Material redArrow;
    [SerializeField] GameObject arrow;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void checkArrow() //change to red/green
    {
        if (cubes > 0) {  arrow.GetComponent<Renderer>().material = greenArrow; }
        else { arrow.GetComponent<Renderer>().material = redArrow; }
    }


    private void OnTriggerEnter(Collider other) //addcubes, make them float
    {
        GameObject obj = other.gameObject;

        if (obj.GetComponent<Weighted>()) {
            cubes++;
            checkArrow();

            if (PlayerManager.player_Pickup.carriedObject == obj) { //player drops cube
                PlayerManager.player_Pickup.Drop();
                
            }

            obj.GetComponent<Rigidbody>().useGravity = false; //cube floats
        }
    }


    private void OnTriggerExit(Collider other) //removecubes
    {
        if (other.GetComponent<Weighted>()) {
            cubes--;
            checkArrow();

            other.gameObject.GetComponent<Rigidbody>().useGravity = true; //stop floaty
        }
    }
}
