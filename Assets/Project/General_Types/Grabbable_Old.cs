using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable_Old : MonoBehaviour {

    private Vector3 spawnPostion;

    private void OnCollisionEnter(Collision collision)
    {
        if (Player_Pickup_Old.Instance.carriedObject != null) {
            Player_Pickup_Old.Instance.dropObject();
        }
    }
    
    private void Start()
    {
        spawnPostion = transform.position;
    }

    public void Reset()
    {
        GetComponent<ParticleSystem>().Emit(40);

        transform.position = spawnPostion;
        if(Player_Pickup_Old.Instance.carriedObject == this.gameObject)
        {
            Player_Pickup_Old.Instance.dropObject();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Player_Pickup_Old.Instance.carriedObject.GetComponent<MeshRenderer>().material = Player_Pickup_Old.Instance.holoError;
        Player_Pickup_Old.Instance.drop = false;
    }
    private void OnTriggerExit(Collider other)
    {
        Player_Pickup_Old.Instance.carriedObject.GetComponent<MeshRenderer>().material = Player_Pickup_Old.Instance.hologram;
        Player_Pickup_Old.Instance.drop = true;
    }
}
