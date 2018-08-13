using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Behaviour : MonoBehaviour {

    public bool isPressed = false;
    public int weight = 0;

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
            weight++;
            Debug.Log(weight);
            isPressed = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Weighted>() != null)
        {
            ButtonCheck();
        }
    }

    public void ButtonCheck() {
        weight--;
        Debug.Log(weight);
        if (weight == 0) {
            isPressed = false; // might cause button to unpress is there were two cubes on it
        }
    }
}
