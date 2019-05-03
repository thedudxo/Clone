using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamButton : MonoBehaviour {

    int cubes = 0;
    public Material greenArrow;
    public Material redArrow;
    public GameObject arrow;
    public Player_Pickup player_Pickup;

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
        if (other.GetComponent<Weighted>()) {
            cubes++;
            checkArrow();

            if (player_Pickup.carriedObject == other) {
                player_Pickup.Drop();
            }

            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other) //removecubes
    {
        if (other.GetComponent<Weighted>()) {
            cubes--;
            checkArrow();
        }
    }
}
