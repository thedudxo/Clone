using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
    
    public int toLevelInt;
    public int fromLevelInt;

    public void SetList() {
        PuzzleManager.beamButton.ChangeList(toLevelInt, fromLevelInt);
        //ButtonLevel.RiseNextLevel(toLevelInt, fromLevelInt);
    }
}
