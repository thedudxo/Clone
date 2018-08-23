using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowOrbit : MonoBehaviour
{
    public float orbitSpeed;
    public Vector3 centreOfOrbit;
    void Update()
        {
            transform.RotateAround(centreOfOrbit, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
