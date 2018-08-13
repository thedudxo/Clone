using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{

    public GameObject button;
    public Animator doorOpenAnimation;

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
            doorOpenAnimation.SetBool("ButtonPush", true);
        }
        else
        {
            doorOpenAnimation.SetBool("ButtonPush", false);
        }
    }
}
