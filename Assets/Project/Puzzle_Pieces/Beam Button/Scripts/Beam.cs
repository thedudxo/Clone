﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {//addcubes, make them float
        GameObject obj = other.gameObject;
        if (obj.GetComponent<Weighted>()) {
            PuzzleManager.beamButton.cubes++;
            PuzzleManager.beamButton.checkArrow();
            obj.GetComponent<Weighted>().overButton = true;
        }
    }

    private void OnTriggerExit(Collider other) {//removecubes

        ButtonLevel level = PuzzleManager.beamButton.CheckCubeHeight(other.gameObject);
        Debug.Log(level.level);
        if (other.GetComponent<Weighted>()) {
            if (level.cubesOverButton.Contains(other.gameObject)) {
                level.ResetCubes(other.gameObject);
            }
            level.cubesOverButton.Remove(other.gameObject);
            PuzzleManager.beamButton.cubes--;
            PuzzleManager.beamButton.checkArrow();
            other.GetComponent<Weighted>().moveRot = false;
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