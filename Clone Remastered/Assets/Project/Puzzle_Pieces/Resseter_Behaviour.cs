using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resseter_Behaviour : MonoBehaviour {

    public bool destroyGrabbables = false;
    private bool isTriggered = false;
    private GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isTriggered)
        {
            player.GetComponent<Player_Clone>().reset();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Grabbable>() != null && destroyGrabbables)
        {
            if(other.gameObject.GetComponent<Cloneable>() != null)
            {
                if (other.gameObject.GetComponent<Cloneable>().isClone)
                {
                    other.gameObject.GetComponent<Cloneable>().destroyClone();
                    player.GetComponent<Player_Clone>().cloned = false;
                }
                else
                {
                    other.gameObject.GetComponent<Grabbable>().Reset();
                }
            }
            else
            {
                other.gameObject.GetComponent<Grabbable>().Reset();
            }
            
        }

        if(other.tag == "Player")
        {
            isTriggered = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTriggered = false;
        }
    }
}
