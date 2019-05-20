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

    public ButtonLevel[] levels;


    private void Start() {
        PuzzleManager.beamButton = this;
        tvscreen = gameObject.GetComponent<TVscreen>();
        tvscreen.displayCubes(cubes);

        levels = new ButtonLevel[] {
            new ButtonLevel(0),
            new ButtonLevel(1)
        };
        
    }

    public void checkArrow() { //change to red/green
        if (cubes > 0) {
            arrow.GetComponent<Renderer>().material = greenArrow;
        } else {
            arrow.GetComponent<Renderer>().material = redArrow;
        }
        tvscreen.displayCubes(cubes);
    }

    public ButtonLevel CheckCubeHeight(GameObject cube) {
        float cubeHeight = cube.transform.position.y;

        int level = Mathf.FloorToInt(cubeHeight / ButtonLevel.levelHeight);
        Debug.Log(level);
        levels[level].cubesOverButton.Add(cube);

        return levels[level];
    }
}
