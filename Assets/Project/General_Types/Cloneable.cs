using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloneable : MonoBehaviour {

    public bool isClone = false;
    private GameObject button;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void destroyClone()
    {
        Debug.Log("KILKLIGN THE CLONE");
        if (Player_Pickup.Instance.carriedObject == this.gameObject)
        {
            Player_Pickup.Instance.dropObject();
            Debug.Log("dropped");
        }

        if(button != null)
        {
            button.GetComponent<Button_Behaviour>().weights--;
            Debug.Log("yuep");
        }

        Destroy(gameObject);
    }

    public void setButton(GameObject button)
    {
        this.button = button;
        Debug.Log(button);
    }
}
