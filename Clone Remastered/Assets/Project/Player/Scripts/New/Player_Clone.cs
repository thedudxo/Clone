using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Clone : MonoBehaviour {

    public bool cloning = false;
    public bool hasCloned = false;
    private int cloneDist = 3;
    private GameObject clonedObject;
    private GameObject mainCamera;
    private GameObject lookingAt;
    private GameObject clipboard;

	void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	void Update () {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        if (Input.GetMouseButtonDown(1)) {
            Scan(x, y);
        }

        if (Input.GetMouseButtonDown(0)) {
            Clone();
        }
	}

    void Scan(int x, int y) {
        Ray cloneRay = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if(Physics.Raycast(cloneRay, out hit)) {
            lookingAt = hit.transform.gameObject;
            if (lookingAt.GetComponent<Cloneable>() != null) {
                clipboard = lookingAt;
                Debug.Log("Can clone");
            } else {
                Debug.Log("Cant clone " + hit.transform.gameObject);
            }
        }
    }

    void Clone() {
        cloning = true;
        clonedObject = Instantiate(clipboard, mainCamera.transform.position + mainCamera.transform.forward * cloneDist, Quaternion.identity);
        clonedObject.name = "Clone";
        clonedObject.GetComponent<Cloneable>().isClone = true;
    }
}