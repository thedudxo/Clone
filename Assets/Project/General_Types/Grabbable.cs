using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    Collider playerCarryCollider;
    GameObject player;
    public bool carried = false;

 

    private void Start()
    {
        playerCarryCollider = PlayerManager.player_Pickup.carrier.GetComponent<SphereCollider>();
        player = PlayerManager.Player;
    }

    private void OnTriggerExit(Collider other) {
        if (other == playerCarryCollider && gameObject == PlayerManager.player_Pickup.carriedObject) {
            PlayerManager.player_Pickup.Drop();
        }
    }
}
