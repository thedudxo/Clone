using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{

<<<<<<< HEAD


    [SerializeField] private GameObject[] buttons;

=======
    public GameObject button;
    public Animator doorOpenAnimation;
>>>>>>> Jordan

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
            doorOpenAnimation.SetBool("ButtonPush", true);
        }
        else
        {
            doorOpenAnimation.SetBool("ButtonPush", false);
        }
    }
}
