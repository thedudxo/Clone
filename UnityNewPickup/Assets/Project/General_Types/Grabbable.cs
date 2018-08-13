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
}
