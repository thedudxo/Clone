using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted : MonoBehaviour {

    private float randomRotation = 1;
    public int buttonPos;
    public bool overButton = false;
    Vector3 moveTo;
    public bool destroyed = false;
    public bool moveRot = false;
    public int distance = 3;

    public void Update() {
        if (moveRot) {
            MovePosition();
        }
    }

    public void Gravity() {
        if(PlayerManager.player_Clone.clonedObject != gameObject && PlayerManager.player_Pickup.carriedObject != gameObject) {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void overBeam(Vector3 buttonPos, int level) {
        moveTo = buttonPos;
        moveTo.y = moveTo.y + distance + (ButtonLevel.levelHeight * level);
    }

    public void MovePosition() {
        gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(gameObject.transform.position, moveTo, Time.deltaTime * 5));
        gameObject.GetComponent<Rigidbody>().AddTorque(
                     Random.Range(-randomRotation, randomRotation),
                     Random.Range(-randomRotation, randomRotation),
                     Random.Range(-randomRotation, randomRotation));
    }
}