using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCubeMovement : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.T)) {
            foreach (GameObject c in ButtonLevel.cubesOverButton) {
                c.GetComponent<Weighted>().distance = c.GetComponent<Weighted>().distance + 10;
                c.GetComponent<Weighted>().overBeam(true, PuzzleManager.beamButton.gameObject.transform.position);
            }
        }
    }
}
