using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public float speed = 10.0F;
	bool onGround = true;
	
	void Start () {
		
		Cursor.lockState = CursorLockMode.Locked;
	
	}
	

	void Update () {
		
		float translation = Input.GetAxis("Vertical") * speed;
		float straffe = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;
		
		transform.Translate (straffe, 0, translation);
		
		if (Input.GetKeyDown("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
		
		//jump
		RaycastHit hit;
		Vector3 physicsCentre = this.transform.position + this.GetComponent<CapsuleCollider>().center;
		
		Debug.DrawRay(physicsCentre, Vector3.down, Color.red, 1);
		if (Physics.Raycast(physicsCentre, Vector3.down, out hit, 1.5f)) {
			if(hit.transform.gameObject.tag != "Player") {
				onGround = true;
                speed = 10f;
			}
		} else {
			onGround = false;
		}
		//Debug.Log(onGround);
		
		
		if (Input.GetKeyDown("space") && onGround) {
			this.GetComponent<Rigidbody>().AddForce(Vector3.up*20000);
		}
	
	}

    void OnCollisionEnter(Collision collision) {
        if (!onGround) {
            speed = 0f;
        }
    }
}
