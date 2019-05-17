using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Pickup : MonoBehaviour {

    private GameObject mainCamera;
    public GameObject carriedObject;
    private float pickupDist = 3;
    public GameObject carrier;
    public float smooth;
    public bool carrying = false;

	void Start () {
        PlayerManager.player_Pickup = this;
        PlayerManager.Player = gameObject;
        mainCamera = GameObject.FindWithTag("MainCamera");
	}


	void FixedUpdate () {
		if (carrying) {
            Carry(carriedObject);
            CheckDrop();
        } else {
            Pickup();
        }
	}

    void Carry(GameObject o) {
        var body = o.GetComponent<Rigidbody>();
        body.MovePosition(Vector3.Lerp(
            body.transform.position,
            carrier.transform.position,
            Time.deltaTime * smooth));
    }

    void CheckDrop() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Drop();
        }
    }

    public void Drop() {
        carriedObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        carrying = false;
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        carrier.GetComponent<SphereCollider>().enabled = false;
        if (!carriedObject.GetComponent<Weighted>().overButton) {
        foreach (GameObject c in PuzzleManager.beamButton.cubesOverButton) {
                c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance - 2;
                c.GetComponent<Weighted>().overBeam(true, PuzzleManager.beamButton.gameObject.transform.position);
            }
            carriedObject.GetComponent<Rigidbody>().useGravity = true;
            carriedObject.GetComponent<Weighted>().distance = 3;
        } else {
            PuzzleManager.beamButton.cubesOverButton.Add(carriedObject);
            carriedObject.GetComponent<Weighted>().movePos = true;
        }
        carriedObject = null;
    }

    void Pickup() {
        if (Input.GetKeyDown(KeyCode.E) && !this.GetComponent<Player_Clone>().cloning) {
            int x = Screen.width / 2;
            int y = Screen.height / 2;
            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Grabbable g = hit.collider.GetComponent<Grabbable>();
                if(g != null && hit.distance <= pickupDist) {
                    if (!g.gameObject.GetComponent<Weighted>().overButton) {
                        foreach (GameObject c in PuzzleManager.beamButton.cubesOverButton) {
                            c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance + 2;
                            c.GetComponent<Weighted>().overBeam(true, PuzzleManager.beamButton.gameObject.transform.position);
                        }
                    }
                    carrying = true;
                    g.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                    g.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    carriedObject = g.gameObject;
                    carriedObject.transform.rotation = Quaternion.identity;
                    carrier.GetComponent<SphereCollider>().enabled = true;
                }
            }
        }
    }
}