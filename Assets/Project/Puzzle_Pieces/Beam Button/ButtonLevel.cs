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

    public void ResetCubes(GameObject cube)
    {
        foreach (ButtonLevel level in PuzzleManager.beamButton.levels) {
            foreach (GameObject c in cubesOverButton) {
                if (level.cubesOverButton.IndexOf(cube) < level.cubesOverButton.IndexOf(c)) {
                    c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance + 2;
                    c.GetComponent<Weighted>().overBeam(true, PuzzleManager.beamButton.gameObject.transform.position);
                }
            }
        }
    }

    public static void ButtonFall() {
        foreach (ButtonLevel level in PuzzleManager.beamButton.levels) {
            foreach (GameObject c in level.cubesOverButton) {
                c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance - 2;
                c.GetComponent<Weighted>().overBeam(true, PuzzleManager.beamButton.gameObject.transform.position);
            }
        }
    }

    public static void ButtonRise() {
        foreach (ButtonLevel level in PuzzleManager.beamButton.levels) {
            foreach (GameObject c in level.cubesOverButton) {
                c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance + 2;
                c.GetComponent<Weighted>().overBeam(true, PuzzleManager.beamButton.gameObject.transform.position);
                Debug.Log(c + "'s number is " + level.cubesOverButton.IndexOf(c));
            }
        }
        
    }
}
