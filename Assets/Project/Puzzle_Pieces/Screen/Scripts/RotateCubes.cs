using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCubes : MonoBehaviour {

    public float randomRotation;

	// Update is called once per frame
	void Update () {
        Debug.Log("yehs");
        gameObject.GetComponent<Rigidbody>().AddTorque(
            Random.Range(-randomRotation, randomRotation),
            Random.Range(-randomRotation, randomRotation),
            Random.Range(-randomRotation, randomRotation)
            );

    }
}
