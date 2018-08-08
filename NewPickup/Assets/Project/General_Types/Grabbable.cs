using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {
    
    private void OnCollisionEnter(Collision collision)
    {
        if (Player_Pickup.Instance.carriedObject != null) {
            Player_Pickup.Instance.dropObject();
        }
    }
}
