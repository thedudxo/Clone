using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterController : MonoBehaviour {

    public float startSpeed = 10f;
    public float jumpPower = 450f;
    float speed;
	bool onGround = true;
    public bool concrete = false;
	
	void Start () {
		
		Cursor.lockState = CursorLockMode.Locked;
        InvokeRepeating("Footsteps", 0.0f, 0.4f);
        speed = startSpeed;
	}
	

	void Update () {
		
		float translation = Input.GetAxis("Vertical") * speed;
		float straffe = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;
		
		transform.Translate (straffe, 0, translation);
		
		if (Input.GetKeyDown("escape")) {
            SceneManager.LoadScene("MainMenu");
  //      	Cursor.lockState = CursorLockMode.None;
        }

        //jump
        RaycastHit hit;
		Vector3 physicsCentre = this.transform.position + this.GetComponent<CapsuleCollider>().center;
		
		Debug.DrawRay(physicsCentre, Vector3.down, Color.red, 1);
		if (Physics.Raycast(physicsCentre, Vector3.down, out hit, 1.5f)) {
			if(hit.transform.gameObject.tag != "Player") {
				onGround = true;
                speed = startSpeed;
			}
		} else {
			onGround = false;
		}

		if(hit.transform.gameObject.tag == "Concrete") {
            concrete = true;
        } else if (hit.transform.gameObject.tag == "Grass")
        {
            concrete = false;
        }
		
		if (Input.GetKeyDown("space") && onGround) {
			this.GetComponent<Rigidbody>().AddForce(Vector3.up*jumpPower);
		}
	
	}

    void OnCollisionEnter(Collision collision) {
        if (!onGround) {
            speed = 0;
        }
    }

    void Footsteps()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 && onGround)
        {
            if (concrete)
            {
                FindObjectOfType<AudioManager>().Play("Concrete_Step");
            }
            else if (!concrete)
            {
                FindObjectOfType<AudioManager>().Play("Grass_Step");
            }

            if (!onGround)
            {
                FindObjectOfType<AudioManager>().CancelInvoke();
            }
        }
    }
}