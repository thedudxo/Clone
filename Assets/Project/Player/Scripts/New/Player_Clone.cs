using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Clone : MonoBehaviour {

    public bool cloning = false;
    public bool hasCloned = false;
    public Transform carrier;
    public float smooth;
    private int cloneDist = 3;
    public GameObject clonedObject;
    private GameObject mainCamera;
    private GameObject lookingAt;
    private GameObject clipboard;

    void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    void Update() {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        if (Input.GetMouseButtonDown(1)) {
            Scan(x, y);
        }

        if (Input.GetMouseButtonDown(0)) {
            if (!cloning) {
                Clone();
            } else {
                Drop();
            }
        }

    }

    void FixedUpdate() {
        if (cloning) {
            CloneCarry(clonedObject);
        }
    }

    void CloneCarry(GameObject o) {
        o.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(o.transform.position, carrier.transform.position, Time.deltaTime * smooth));
    }

    void Drop() {
        clonedObject.GetComponent<Rigidbody>().freezeRotation = false;
        clonedObject.GetComponent<Rigidbody>().useGravity = true;
        cloning = false;
    }

    void Scan(int x, int y) {
        Ray cloneRay = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(cloneRay, out hit)) {
            lookingAt = hit.transform.gameObject;
            if (lookingAt.GetComponent<Cloneable>() != null) {
                clipboard = lookingAt;
                Debug.Log("Can clone");
            }
            else
            {
                Debug.Log("Cant clone " + hit.transform.gameObject);
            }
        }
    }

    void Clone() {
        if (hasCloned) {
            Destroy(clonedObject);
        }
        if (clipboard != null) {
            cloning = true;
            clonedObject = Instantiate(clipboard, mainCamera.transform.position + mainCamera.transform.forward * cloneDist, Quaternion.identity);
            clonedObject.GetComponent<Rigidbody>().freezeRotation = true;
            clonedObject.GetComponent<Rigidbody>().useGravity = false;
            clonedObject.name = "Clone";
            clonedObject.GetComponent<Cloneable>().isClone = true;
            hasCloned = true;
        }
    }
}