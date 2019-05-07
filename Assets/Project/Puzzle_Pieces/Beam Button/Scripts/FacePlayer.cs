using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {


	// Update is called once per frame
	void Update () {

        //face the player
        transform.LookAt(PlayerManager.Player.transform.position);

        // stop rotating upwards
        var  rotate = transform.eulerAngles; rotate.x = 0;
        transform.rotation = Quaternion.Euler(rotate);
    }
}
