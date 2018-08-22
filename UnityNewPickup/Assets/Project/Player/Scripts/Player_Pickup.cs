using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Pickup : MonoBehaviour {

    public GameObject mainCamera;
    public bool carrying;
    public GameObject carriedObject;
    public float distance;
    public float smooth;
    public bool hasPlayer;
    public bool cloning = false;
    float pickupDistance = 3;
    public bool drop = true;

    //materials
    public Material cloneMaterial;
    public Material hologram;
    public Material holoError;

    private static Player_Pickup instance;

    public static Player_Pickup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player_Pickup>();
            }
            return Player_Pickup.instance;
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
        o.transform.parent = mainCamera.transform;
        o.transform.rotation = Quaternion.Euler(0, o.transform.rotation.y, 0);
        o.GetComponent<Rigidbody>().freezeRotation = true;
        if(o.transform.position.y < mainCamera.transform.position.y -1.5f)
        {
            o.transform.position = new Vector3(o.transform.position.x, mainCamera.transform.position.y - 1.5f, o.transform.position.z);
        }

        if (Input.GetAxis ("Mouse ScrollWheel") > 0 && cloning) {
            if (distance < 8) {
                distance++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cloning)
        {
            if (distance > 2){
                distance--;
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            distance = 3;
        }
    }

    void pickup() {
        if(Input.GetKeyDown(KeyCode.E)) {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                Grabbable g = hit.collider.GetComponent<Grabbable>();
                if(g != null && hit.distance <= pickupDistance) {
                    carrying = true;
                    g.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    carriedObject = g.gameObject;
                    carriedObject.transform.rotation = Quaternion.identity;
                }
            }
        }
    }

    void checkDrop() {
        if (Input.GetKeyDown (KeyCode.E) && drop) {
            dropObject();
        }
    }

    public void dropObject() {
<<<<<<< HEAD:UnityNewPickup/Assets/Project/Player/Player_Pickup.cs
        carrying = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject = null;
        cloning = false;
        distance = 2;
    }
}

=======
>>>>>>> master:UnityNewPickup/Assets/Project/Player/Scripts/Player_Pickup.cs
        carrying = false;
        if (cloning)
        {
            carriedObject.GetComponent<MeshRenderer>().material = cloneMaterial;
            carriedObject.GetComponent<BoxCollider>().isTrigger = false;
            carriedObject.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.On;
        }
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        carriedObject.transform.parent = null;
        carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject = null;
        cloning = false;
<<<<<<< HEAD:UnityNewPickup/Assets/Project/Player/Player_Pickup.cs
        distance = 3;
=======
        distance = 3;
        
    }
}

>>>>>>> master:UnityNewPickup/Assets/Project/Player/Scripts/Player_Pickup.cs
