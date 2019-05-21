using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted : MonoBehaviour {

    private float randomRotation = 1;
    public int iD;
    public bool overButton = false;
    Vector3 moveTo;
    public bool destroyed = false;
    public bool movePos = false;
    public int distance = 3;

    public void Update() {
        if (movePos) {
            MovePosition();
        }
    }

    public void ChangeIndex() {
            iD = ButtonLevel.cubesOverButton.IndexOf(gameObject);
    }

    public void Gravity() {
        if(PlayerManager.player_Clone.clonedObject != gameObject && PlayerManager.player_Pickup.carriedObject != gameObject) {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void overBeam(bool overButton, Vector3 buttonPos) {
        this.overButton = overButton;
        moveTo = buttonPos;
        moveTo.y = moveTo.y + distance;
    }

    public void MovePosition() {
        gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(gameObject.transform.position, moveTo, Time.deltaTime * 5));
        gameObject.GetComponent<Rigidbody>().AddTorque(
                     Random.Range(-randomRotation, randomRotation),
                     Random.Range(-randomRotation, randomRotation),
                     Random.Range(-randomRotation, randomRotation));
    }
}