using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCubes : MonoBehaviour {

    public float randomRotation;

	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Rigidbody>().AddTorque(
            Random.Range(-randomRotation, randomRotation),
            Random.Range(-randomRotation, randomRotation),
            Random.Range(-randomRotation, randomRotation)
            );

    }
}
