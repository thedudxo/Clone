using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamButton : MonoBehaviour {

    public int cubes = 0;
    public bool overButton;
    [SerializeField] Material greenArrow;
    [SerializeField] Material redArrow;
    [SerializeField] GameObject arrow;

    private void Start() {
        PuzzleManager.beamButton = this;
    }

    public void checkArrow() { //change to red/green
        if (cubes > 0) {
            arrow.GetComponent<Renderer>().material = greenArrow;
        } else {
            arrow.GetComponent<Renderer>().material = redArrow;
        }
    }
}
