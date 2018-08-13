using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickup : MonoBehaviour {

    public Transform player;
    public Transform playerCam;
    bool hasPlayer = false;
    bool beingCarried = false;
    bool touched = false;
	
	void Update () {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f) {
            hasPlayer = true;
        } else {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetKey(KeyCode.E)) {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }
        if (beingCarried) {
            if (touched) {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.E)) {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            beingCarried = false;
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (beingCarried) {
            touched = true;
        }
    }
}
