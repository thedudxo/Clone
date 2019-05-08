using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {


    private void OnTriggerEnter(Collider other) {//addcubes, make them float
        GameObject obj = other.gameObject;
        if (obj.GetComponent<Weighted>()) {
            PuzzleManager.beamButton.cubes++;
            PuzzleManager.beamButton.checkArrow();
            PuzzleManager.beamButton.overButton = true;
        }
    }

    private void OnTriggerExit(Collider other) {//removecubes
        if (other.GetComponent<Weighted>()) {
            PuzzleManager.beamButton.cubes--;
            PuzzleManager.beamButton.checkArrow();
            StartCoroutine(WaitTrigger());
        }
    }

    IEnumerator WaitTrigger() {
        yield return new WaitForEndOfFrame();
        PuzzleManager.beamButton.overButton = false;
    }
}
