using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {
    
    private void OnCollisionEnter(Collision collision)
    {
        if (PickUp.Instance.carriedObject != null) {
            PickUp.Instance.dropObject();
        }
    }
}
