using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //face the player
        transform.LookAt(target);

        // stop rotating upwards
        var  rotate = transform.eulerAngles; rotate.x = 0;
        transform.rotation = Quaternion.Euler(rotate);
    }
}
