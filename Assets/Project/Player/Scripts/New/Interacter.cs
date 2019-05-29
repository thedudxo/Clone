using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour {

    private GameObject mainCamera;
    private float interDist = 4;

    private void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.E)) {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                Interact i = hit.collider.GetComponent<Interact>();
                if(i != null && hit.distance <= interDist) {
                    i.SetList();
                }
            }
        }
    }
}
