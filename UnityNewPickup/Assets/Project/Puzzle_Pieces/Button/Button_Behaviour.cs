using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Behaviour : MonoBehaviour {

    public bool isPressed = false;
    public int weights = 0;
    public Animator ButtonAnim;

    private static Button_Behaviour instance;

    public static Button_Behaviour Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Button_Behaviour>();
            }
            return Button_Behaviour.instance;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Weighted>() != null){
            ButtonAnim.SetBool("ButtonPress", true);
            weights++;
            isPressed = true;
            Debug.Log(weights);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Weighted>() != null)
        {
            weights--;
            Debug.Log(weights);
            if (weights == 0) {
                ButtonAnim.SetBool("ButtonPress", false);
                isPressed = false;
            }
        }
    }
  
}
