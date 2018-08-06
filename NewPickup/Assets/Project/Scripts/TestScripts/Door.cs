using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{

    public GameObject button;

    // Use this for initialization
    void Start () {
		if(button.GetComponent<Button_Behaviour>() == null)
        {
            throw new MissingComponentException();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (button.GetComponent<Button_Behaviour>().isPressed)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
    }
}
