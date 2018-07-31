using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Behaviour : MonoBehaviour {

    public List<IToggleableObject> linked;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Weighted>() != null){
            Debug.Log("yes! :D");
            foreach(IToggleableObject puzzlepeice in linked)
            {
                puzzlepeice.Toggle();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Weighted>() != null)
        {
            Debug.Log("No :c");
            foreach (IToggleableObject puzzlepeice in linked)
            {
                puzzlepeice.Toggle(false);
            }
        }
    }


}
