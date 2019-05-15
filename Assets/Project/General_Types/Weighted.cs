using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted : MonoBehaviour {

    public bool overButton = false;
    public bool destroyed = false;
    public int distance = 5;

    public void Gravity() {
        if(PlayerManager.player_Clone.clonedObject != gameObject && PlayerManager.player_Pickup.carriedObject != gameObject) {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void MovePosition() {

    }
}