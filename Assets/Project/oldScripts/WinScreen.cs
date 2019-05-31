using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject youMadeIt;

    void Update()
    {
        if (gameObject.tag == "End" && Button_Behaviour.Instance.isPressed == true)
        {
            youMadeIt.SetActive(true);
        }
    }
	
	
}
