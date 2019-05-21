using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamButton : MonoBehaviour {

    //this script detects how many cubes are in the button, and holds the cubes there.

    public int cubes = 0;
    [SerializeField] Material greenArrow;
    [SerializeField] Material redArrow;
    [SerializeField] GameObject arrow;

    TVscreen tvscreen;

    ButtonLevel level1;
    ButtonLevel level2;

    private void Start() {
        PuzzleManager.beamButton = this;
        tvscreen = gameObject.GetComponent<TVscreen>();
        tvscreen.displayCubes(cubes);

        level1 = new ButtonLevel(1);
        level2 = new ButtonLevel(2);
    }

    public void checkArrow() { //change to red/green
        if (cubes > 0) {
            arrow.GetComponent<Renderer>().material = greenArrow;
        } else {
            arrow.GetComponent<Renderer>().material = redArrow;
        }
        tvscreen.displayCubes(cubes);
    }
}
