using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealCubes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EwokDestinations.goDeliverCube = true;
        }
    }

}
