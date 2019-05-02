using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    public Collider dropMe;
    public GameObject player;

    private void OnTriggerExit(Collider other) {
        if (other == dropMe) {
            player.GetComponent<Player_Pickup>().Drop();
        }
    }

}
