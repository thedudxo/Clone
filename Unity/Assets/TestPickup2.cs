using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickup2 : MonoBehaviour {

    GameObject mainCamera;
    bool carrying;
    GameObject carriedObject;
    public float distance;
    public float smooth;
    public bool hasPlayer;

    private static TestPickup2 instance;

    public static TestPickup2 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TestPickup2>();
            }
            return TestPickup2.instance;
        }
    }

    void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	void Update () {
        if (carrying) {
            carry(carriedObject);
            checkDrop();
        } else {
            pickup();
        }
	}

    void carry (GameObject o) {
        o.transform.position = Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
        o.transform.rotation = Quaternion.identity;
    }

    void pickup() {
        if(Input.GetKeyDown(KeyCode.E) && hasPlayer) {
            Debug.Log("Pick up");
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                Grabbable g = hit.collider.GetComponent<Grabbable>();
                if(g != null) {
                    carrying = true;
                    g.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    carriedObject = g.gameObject;
                }
            }
        }
    }

    void checkDrop() {
        if (Input.GetKeyDown (KeyCode.E)) {
            dropObject();
        }
    }

    void dropObject() {
        carrying = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject = null;
    }
}
