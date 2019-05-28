using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamButton : MonoBehaviour {

    //this script detects how many cubes are in the button, and holds the cubes there.

    public int cubes = 0;
    public static GameObject buttonButton;
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

    public void CheckArrow() { //change to red/green
        if (cubes > 0) {
            arrow.GetComponent<Renderer>().material = greenArrow;
        } else {
            arrow.GetComponent<Renderer>().material = redArrow;
        }
        tvscreen.displayCubes(cubes);
    }

    public ButtonLevel CheckCubeHeight(GameObject cube) {
        //Checks height of cube and adds the object to the correct level list
        float cubeHeight = cube.transform.position.y;
        if(cubeHeight < 0) { cubeHeight = 0; }
        int level = Mathf.FloorToInt(cubeHeight / ButtonLevel.levelHeight);
        cube.GetComponent<Weighted>().overBeam(gameObject.transform.position, level);
        return levels[level];
    }

    public void ChangeList(int toLevel, int fromLevel) {
        levels[toLevel].RiseNextLevel(levels[fromLevel].cubesOverButton.Count);
        foreach (GameObject c in levels[fromLevel].cubesOverButton) {
            levels[toLevel].cubesOverButton.Add(c);
            c.GetComponent<Weighted>().overBeam(gameObject.transform.position, toLevel);
        }
        levels[fromLevel].cubesOverButton.Clear();
        Debug.Log(levels[fromLevel].cubesOverButton.Count);
    }

    public void AddCube(GameObject cube, int level) {
        levels[level].cubesOverButton.Add(cube);
    }
}
