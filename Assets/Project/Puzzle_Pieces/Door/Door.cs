using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{

    [SerializeField] private BeamButton[] buttons;
    [SerializeField] private int cubesRequired; //set to a negative number for regular buttonage
    [SerializeField] Collider doorCollider;
    Animator doorOpenAnimation;

    void Start () {
        doorOpenAnimation = GetComponent<Animator>();
	}
	
	void Update () {

        int buttonsPressed = 0;
        foreach (BeamButton button in buttons) { 
            if (button.cubes == cubesRequired)
            {
                buttonsPressed++;
            }
        }

        if (buttonsPressed >= buttons.Length)
        {
            doorOpenAnimation.SetBool("ButtonPush", true);
            doorCollider.enabled = false;
        }
        else
        {
            doorCollider.enabled = true;
            doorOpenAnimation.SetBool("ButtonPush", false);
        }
    }
}
