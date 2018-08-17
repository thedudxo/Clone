using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallRespawn : MonoBehaviour {

	void Update () {
	    if (gameObject.transform.position.y <= -200)
	    {
	        gameObject.transform.position = new Vector3(70f,3.5f, 20f);
	    }

    }
}
