using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Behaviour : MonoBehaviour {

    public bool isPressed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Weighted>() != null){

            isPressed = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Weighted>() != null)
        {
            isPressed = false; // might cause button to unpress is there were two cubes on it
        }
    }


}
