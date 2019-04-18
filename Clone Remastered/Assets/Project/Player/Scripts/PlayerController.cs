using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float accel = 1000;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveVertical * accel, 0, moveHorizontal * accel);
        rb.AddRelativeForce(moveHorizontal * accel * Time.deltaTime, 0, moveVertical * accel * Time.deltaTime);

        Debug.Log(rb.velocity.magnitude);
    }
}
