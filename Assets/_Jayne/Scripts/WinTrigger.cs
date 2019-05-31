using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour {

    public static bool isWin;

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            isWin = true;
        }
    }
}
