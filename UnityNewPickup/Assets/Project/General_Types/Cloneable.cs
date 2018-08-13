using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloneable : MonoBehaviour {

    public bool isClone = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void destroyClone()
    {
        if (Player_Pickup.Instance.carriedObject == this.gameObject)
        {
            Player_Pickup.Instance.dropObject();
        }
        Destroy(gameObject);
    }
}
