using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevel {

    public List<GameObject> cubesOverButton = new List<GameObject>();
    public int level;
    public static readonly float levelHeight = 13;

    public ButtonLevel(int level) {
        this.level = level;
    }

    public void ResetCubes(GameObject cube) {
        foreach (ButtonLevel level in PuzzleManager.beamButton.levels) {
            foreach (GameObject c in cubesOverButton) {
                if (level.cubesOverButton.IndexOf(cube) < level.cubesOverButton.IndexOf(c)) {
                    c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance + 2;
                    c.GetComponent<Weighted>().overBeam(PuzzleManager.beamButton.gameObject.transform.position, this.level);
                }
            }
        }
    }

    public static void ButtonFall() {
        //drops all cubes in the button
        foreach (ButtonLevel level in PuzzleManager.beamButton.levels) {
            foreach (GameObject c in level.cubesOverButton) {
                c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance - 2;
                c.GetComponent<Weighted>().overBeam(PuzzleManager.beamButton.gameObject.transform.position, level.level);
            }
        }
    }

    public static void ButtonRise() {
        //raises all cubes in button
        foreach (ButtonLevel level in PuzzleManager.beamButton.levels) {
            foreach (GameObject c in level.cubesOverButton) {
                c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance + 2;
                c.GetComponent<Weighted>().overBeam(PuzzleManager.beamButton.gameObject.transform.position, level.level);
            }
        }
    }

    public static void DropLevelCubes(GameObject cube) {
        //Drops cubes if the cube added isn't in the same list
        foreach(ButtonLevel level in PuzzleManager.beamButton.levels) {
            if (!level.cubesOverButton.Contains(cube)) {
                foreach (GameObject c in level.cubesOverButton) {
                    c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance - 2;
                    c.GetComponent<Weighted>().overBeam(PuzzleManager.beamButton.gameObject.transform.position, level.level);
                }
            }
        }
    }

    public static void RiseIndivCube(GameObject cube) {
        foreach (ButtonLevel level in PuzzleManager.beamButton.levels) {
            if (!level.cubesOverButton.Contains(cube)) {
                foreach (GameObject c in level.cubesOverButton) {
                    c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance + 2;
                    c.GetComponent<Weighted>().overBeam(PuzzleManager.beamButton.gameObject.transform.position, level.level);
                }
            }
        }
    }
}