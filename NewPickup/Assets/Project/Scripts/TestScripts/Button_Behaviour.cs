using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Behaviour : MonoBehaviour {

    public bool isPressed = false;

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
            Debug.Log("Press button");
            isPressed = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Weighted>() != null)
        {
            isPressed = false;
        }
    }
  
}
