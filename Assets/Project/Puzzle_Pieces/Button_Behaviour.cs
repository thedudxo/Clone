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

    private void Update()
    {
        if(weights > 0)
        {
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Weighted>() != null){
            ButtonAnim.SetBool("ButtonPress", true);
            weights++;
            isPressed = true;

            if(collision.gameObject.GetComponent<Cloneable>() != null)
            {
                Debug.Log("need setButton method");
//                collision.gameObject.GetComponent<Cloneable>().setButton(gameObject);
            }

        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Weighted>() != null)
        {
            weights--;
            if (weights == 0) {
                ButtonAnim.SetBool("ButtonPress", false);
                isPressed = false;
            }

            if (collision.gameObject.GetComponent<Cloneable>() != null)
            {
                //collision.gameObject.GetComponent<Cloneable>().setButton(null);
            }
        }
    }
  
}
