using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {
    
    public void CheckGravity() {
        if(this == PlayerManager.player_Clone.clonedObject && this == PlayerManager.player_Pickup.carriedObject){
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
