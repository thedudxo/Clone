using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{



    [SerializeField] private GameObject[] buttons;


    // Use this for initialization
    void Start () {
        foreach(GameObject button in buttons)
        {
            if (button.GetComponent<Button_Behaviour>() == null)
            {
                throw new MissingComponentException();
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        int buttonsPressed = 0;
        foreach (GameObject button in buttons)
        {
            if (button.GetComponent<Button_Behaviour>().isPressed)
            {
                buttonsPressed++;
            }
        }

        if (buttonsPressed >= buttons.Length)
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
