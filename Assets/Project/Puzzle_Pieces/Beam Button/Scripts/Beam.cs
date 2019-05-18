using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {//addcubes, make them float
        GameObject obj = other.gameObject;
        if (obj.GetComponent<Weighted>()) {
            PuzzleManager.beamButton.cubes++;
            PuzzleManager.beamButton.checkArrow();
            obj.GetComponent<Weighted>().overBeam(true, transform.position);
        }
    }

    private void OnTriggerExit(Collider other) {//removecubes
        if (other.GetComponent<Weighted>()) {
            if (ButtonLevel.cubesOverButton.Contains(other.gameObject)) {
                ButtonLevel.ResetCubes(other.gameObject);
            }
            ButtonLevel.cubesOverButton.Remove(other.gameObject);
            foreach(GameObject c in ButtonLevel.cubesOverButton) {
                c.GetComponent<Weighted>().ChangeIndex();
            }
            PuzzleManager.beamButton.cubes--;
            PuzzleManager.beamButton.checkArrow();
            other.GetComponent<Weighted>().movePos = false;
            other.GetComponent<Weighted>().Gravity();
            StartCoroutine(WaitTrigger(other));
        }
    }

    IEnumerator WaitTrigger(Collider other) {
        if (other.GetComponent<Weighted>().destroyed) { yield break; }
        yield return new WaitForEndOfFrame();
        other.GetComponent<Weighted>().overButton = false;
    }
}