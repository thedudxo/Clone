using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float accel = 1000;
    [SerializeField] private float airAccel = 0.1f;
    [SerializeField] private float deccel = 5;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float jumpForce = 800;
    [SerializeField] private float maxSlope = 60;
    private Rigidbody rb;
    private Vector2 horizontalMovement;
    private bool grounded = false;
    private float deccelX = 0;
    private float deccelZ = 0;


    void Start() {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void FixedUpdate() {
        //Max Speed
        horizontalMovement = new Vector2(rb.velocity.x, rb.velocity.z);
        if(horizontalMovement.magnitude > maxSpeed) {
            horizontalMovement = horizontalMovement.normalized * maxSpeed;
        }
        rb.velocity = new Vector3(horizontalMovement.x, rb.velocity.y, horizontalMovement.y);
        //Decceleration
        var vel = rb.velocity;
        if(grounded) {
            vel.x = Mathf.SmoothDamp(vel.x, 0, ref deccelX, deccel);
            vel.z = Mathf.SmoothDamp(vel.z, 0, ref deccelZ, deccel);
            rb.velocity = vel;
        }
        //Movement
        if (grounded) {
            rb.AddRelativeForce(Input.GetAxis("Horizontal") * accel * Time.deltaTime, 0, Input.GetAxis("Vertical") * accel * Time.deltaTime);
        } else {
            rb.AddRelativeForce(Input.GetAxis("Horizontal") * accel * airAccel * Time.deltaTime, 0, Input.GetAxis("Vertical") * accel * airAccel * Time.deltaTime);
        }
        //Jump
        if(Input.GetButtonDown("Jump") && grounded) {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    private void OnCollisionStay(Collision collision) {
        foreach(ContactPoint contact in collision.contacts) {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope) {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        grounded = false;
    }

    private void OnTriggerEnter(Collider other) {
        var vel = rb.velocity;
        if(other.transform.tag == "UpStair") {
            if(grounded && Vector3.Angle(rb.velocity, other.transform.forward) < 90) {
                if(rb.velocity.y > 0) {
                    Debug.Log("Trigger");
                    vel.y = 0;
                    rb.velocity = vel;
                }
            }
        }
        if (other.transform.tag == "DownStair") {
            if (grounded && Vector3.Angle(rb.velocity, other.transform.forward) < 90) {
                rb.AddForce(0, -1000, 0);
            }
        }
    }
}
