using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

    public float startPos;
    public float endPos;
    public float speed;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        float newY = transform.position.y + speed;

        if (transform.position.y < endPos)
        {
            newY = startPos;
        }

        transform.position = new Vector3(
                    transform.position.x,
                    newY,
                    transform.position.z);
    }
}
