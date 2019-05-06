using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouseLook : MonoBehaviour {

	public float mouseSensitivity;
	public GameObject playerBody;
	
	float xAxisClamp = 0;
	
	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update () {
		RotateCamera();
        if(transform.rotation.eulerAngles.x >= 50 && transform.rotation.eulerAngles.x <= 269 && playerBody.GetComponent<Player_Pickup>().carrying) { playerBody.GetComponent<Player_Pickup>().Drop(); }
	}
	
	
	void RotateCamera () {
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		
		float rotAmountX = mouseX * mouseSensitivity;
		float rotAmountY = mouseY * mouseSensitivity;
		
		xAxisClamp -= rotAmountY;
		
		Vector3 targetRotCam = transform.rotation.eulerAngles;
		Vector3 targetRotBody = playerBody.transform.rotation.eulerAngles;
		
		targetRotCam.x -= rotAmountY;
		targetRotCam.z = 0;
		targetRotBody.y += rotAmountX;
		
		if(xAxisClamp > 90) {
			
			xAxisClamp = 90;
			targetRotCam.x = 90;
		
		} else if (xAxisClamp < -90) {
			
			xAxisClamp = -90;
			targetRotCam.x = 270;
		
		}
		
		transform.rotation = Quaternion.Euler(targetRotCam);
		playerBody.transform.rotation = Quaternion.Euler(targetRotBody);
	}
}
