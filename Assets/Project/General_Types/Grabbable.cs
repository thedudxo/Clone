using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    public Collider dropMe;
    public GameObject player;
    public bool carried = false;

    private void OnTriggerExit(Collider other) {
        if (other == dropMe && this == PlayerManager.player_Pickup.carriedObject) {
            PlayerManager.player_Pickup.Drop();
        }
    }
}
