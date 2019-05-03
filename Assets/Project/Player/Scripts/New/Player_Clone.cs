using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Clone : MonoBehaviour {

    private GameObject mainCamera;
    private GameObject lookingAt;
    private GameObject clipboard;
    private int cloneDist = 3;
    private float lerp = 0;
    private string dissolveAnim = "Vector1_FA6C32DC";
    public Transform carrier;
    public GameObject clonedObject;
    public Material dissolve;
    public bool cloning = false;
    public bool hasCloned = false;
    public float smooth;

    void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        dissolve.SetFloat(dissolveAnim, 1.1f);
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
            lerp += Time.deltaTime;
            dissolve.SetFloat(dissolveAnim, Mathf.Lerp(0.5f, -1.1f, lerp));
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
        if (clipboard != null && this.GetComponent<Player_Pickup>().carrying == false) {
            if (hasCloned) {
                Destroy(clonedObject);
            }
            cloning = true;
            clonedObject = Instantiate(clipboard, mainCamera.transform.position + mainCamera.transform.forward * cloneDist, Quaternion.identity);
            clonedObject.GetComponent<Renderer>().material = dissolve;
            clonedObject.GetComponent<Rigidbody>().freezeRotation = true;
            clonedObject.GetComponent<Rigidbody>().useGravity = false;
            clonedObject.name = "Clone";
            clonedObject.GetComponent<Cloneable>().isClone = true;
            hasCloned = true;
        }
    }
}