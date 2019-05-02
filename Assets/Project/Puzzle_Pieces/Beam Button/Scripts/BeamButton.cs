using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamButton : MonoBehaviour {

    int cubes = 0;
    public Material greenArrow;
    public Material redArrow;
    public GameObject arrow;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void checkArrow()
    {
        if (cubes > 0)
        {
            arrow.GetComponent<Renderer>().material = greenArrow;
        }
        else
        {
            arrow.GetComponent<Renderer>().material = redArrow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("yesh");
        if (other.GetComponent<Weighted>())
        {
            cubes++;
            checkArrow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Weighted>())
        {
            cubes--;
            checkArrow();
        }
    }
}
