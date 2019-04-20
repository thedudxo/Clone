using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float accel = 1000;
    public float airAccel = 0.1f;
    public float deccel = 5;
    public float maxSpeed = 20;
    public float jumpForce = 800;
    public float maxSlope = 60;
    private Rigidbody rb;
    private Vector2 horizontalMovement;
    private bool grounded = false;
    private float deccelX = 0;
    private float deccelZ = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        float speed = rb.velocity.magnitude;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveVertical * accel, 0, moveHorizontal * accel);
        if (grounded) {
            //Relative force push in the direction player is facing.
            rb.AddRelativeForce(moveHorizontal * accel * Time.deltaTime, 0, moveVertical * accel * Time.deltaTime, ForceMode.VelocityChange);
        } else {
            //if not grounded limit the movement. also prevents sticking to walls in the air.
            rb.AddRelativeForce(moveHorizontal * accel * airAccel * Time.deltaTime, 0, moveVertical * accel * airAccel * Time.deltaTime);
        }
        var vel = rb.velocity;
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && grounded) {
            vel.x = Mathf.SmoothDamp(rb.velocity.x, 0, ref deccelX, deccel);
            vel.z = Mathf.SmoothDamp(rb.velocity.z, 0, ref deccelZ, deccel);
            rb.velocity = vel;
        }
        /* ---SET MAX SPEED--- */
        // use vector2 so if the character falls the max speed doesnt affect the local y axis
        horizontalMovement = new Vector2(rb.velocity.x, rb.velocity.z);
        if (horizontalMovement.magnitude > maxSpeed) {
            horizontalMovement = horizontalMovement.normalized * maxSpeed;
        }
        rb.velocity = new Vector3(horizontalMovement.x, rb.velocity.y, horizontalMovement.y);
        /* ---SET MAX SPEED--- */

        if (Input.GetButtonDown("Jump") && grounded) {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    private void OnCollisionStay(Collision collision) {
        foreach (ContactPoint point in collision.contacts) {
            if (Vector3.Angle(point.normal, Vector3.up) <= maxSlope) {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        grounded = false;
    }
}
