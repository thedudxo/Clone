using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{

    [SerializeField] private GameObject[] buttons;
    
    public Animator doorOpenAnimation;
    
    void Start () {
        foreach(GameObject button in buttons)
        {
            if (button.GetComponent<Button_Behaviour>() == null)
            {
                throw new MissingComponentException();
            }
        }
		
	}
	
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
            doorOpenAnimation.SetBool("ButtonPush", true);
        }
        else
        {
            doorOpenAnimation.SetBool("ButtonPush", false);
        }
    }
}
