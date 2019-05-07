using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Clone : MonoBehaviour {

    private GameObject mainCamera;
    private GameObject lookingAt;
    private GameObject clipboard;
    private Renderer renderer;
    private Rigidbody cloneRb;
    private GameObject prevClone;
    public bool canClone = true;
    private int cloneDist = 3;
    public GameObject clonedObject;
    public bool cloning = false;
    public bool hasCloned = false;
    public float smooth;

    void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        canClone = true;
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
            } else if (cloning && canClone){
                Drop();
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (cloning && canClone) {
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
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Vector3 direction = (clonedObject.transform.position - mainCamera.transform.position).normalized;
        Ray cloneRay = new Ray(mainCamera.transform.position, direction * cloneDist);
        RaycastHit hit;
        Physics.Raycast(cloneRay, out hit);
        if (hit.collider.gameObject != clonedObject) {
            canClone = false;
        } else if (hit.collider.gameObject == clonedObject && clonedObject.GetComponent<Cloneable>().triggers == 0)
        {
            canClone = true;
        }
            o.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * cloneDist, Time.deltaTime * smooth));
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && cloneDist != 10) {
            cloneDist++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cloneDist != 2) {
            cloneDist--;
        }
        Debug.DrawRay(mainCamera.transform.position, direction * cloneDist);
    }

    void Drop() {
        cloneRb.freezeRotation = false;
        cloneRb.useGravity = true;
        renderer.shadowCastingMode = ShadowCastingMode.On;
        renderer.receiveShadows = true;
        clonedObject.GetComponent<BoxCollider>().isTrigger = false;
        prevClone = clonedObject;
        clonedObject = null;
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
        if (clipboard != null && this.GetComponent<Player_Pickup>().carrying == false) {
            if (hasCloned) {
                Destroy(prevClone);
            }
            cloning = true;
            clonedObject = Instantiate(clipboard, mainCamera.transform.position + mainCamera.transform.forward * cloneDist, Quaternion.identity);
            renderer = clonedObject.GetComponent<Renderer>();
            cloneRb = clonedObject.GetComponent<Rigidbody>();
            renderer.shadowCastingMode = ShadowCastingMode.Off;
            renderer.receiveShadows = false;
            cloneRb.freezeRotation = true;
            cloneRb.useGravity = false;
            clonedObject.GetComponent<BoxCollider>().isTrigger = true;
            clonedObject.name = "Clone";
            clonedObject.GetComponent<Cloneable>().isClone = true;
            hasCloned = true;
        }
    }
}