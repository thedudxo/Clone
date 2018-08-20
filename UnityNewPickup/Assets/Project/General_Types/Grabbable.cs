using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    private Vector3 spawnPostion;

    private void OnCollisionEnter(Collision collision)
    {
        if (Player_Pickup.Instance.carriedObject != null) {
            Player_Pickup.Instance.dropObject();
        }
    }

<<<<<<< HEAD
    private void Start()
    {
        spawnPostion = transform.position;
    }

    public void Reset()
    {
        transform.position = spawnPostion;
        if(Player_Pickup.Instance.carriedObject == this.gameObject)
        {
            Player_Pickup.Instance.dropObject();
        }

    }

=======
>>>>>>> Testbuild
    private void Start()
    {
        spawnPostion = transform.position;
    }

    public void Reset()
    {
        transform.position = spawnPostion;
        if (Player_Pickup.Instance.carriedObject == this.gameObject)
        {
            Player_Pickup.Instance.dropObject();
        }

    }
<<<<<<< HEAD
}
=======
>>>>>>> Testbuild

    private void OnTriggerEnter(Collider other)
    {
        Player_Pickup.Instance.carriedObject.GetComponent<MeshRenderer>().material = Player_Pickup.Instance.holoError;
        Player_Pickup.Instance.drop = false;
<<<<<<< HEAD
    private void OnTriggerExit(Collider other)
    {
        Player_Pickup.Instance.carriedObject.GetComponent<MeshRenderer>().material = Player_Pickup.Instance.hologram;
        Player_Pickup.Instance.drop = true;
=======

    }

    private void OnTriggerExit(Collider other)
    {
        Player_Pickup.Instance.carriedObject.GetComponent<MeshRenderer>().material = Player_Pickup.Instance.hologram;
        Player_Pickup.Instance.drop = true;
    }
}
>>>>>>> Testbuild
