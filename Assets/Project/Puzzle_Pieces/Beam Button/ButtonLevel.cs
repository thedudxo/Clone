using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevel {

    public static List<GameObject> cubesOverButton = new List<GameObject>();
    public int level = 1;

    public ButtonLevel(int level) {
        this.level = level;
    }

    public static void ResetCubes(GameObject cubeID) {
        foreach (GameObject c in cubesOverButton) {
            if(cubeID.GetComponent<Weighted>().iD < c.GetComponent<Weighted>().iD) {
                c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance + 2;
                c.GetComponent<Weighted>().overBeam(true, PuzzleManager.beamButton.gameObject.transform.position);
            }
        }
    }

    public static void ButtonFall() {
        foreach (GameObject c in cubesOverButton) {
            c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance - 2;
            c.GetComponent<Weighted>().overBeam(true, PuzzleManager.beamButton.gameObject.transform.position);
        }
    }

    public static void ButtonRise() {
        foreach (GameObject c in cubesOverButton) {
            c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance + 2;
            c.GetComponent<Weighted>().overBeam(true, PuzzleManager.beamButton.gameObject.transform.position);
            Debug.Log(c + "'s number is " + cubesOverButton.IndexOf(c));
        }
    }
}
