using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update () {
        float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (dist <= 2.5f)
        {
            TestPickup2.Instance.hasPlayer = true;
        }
        else
        {
            TestPickup2.Instance.hasPlayer = false;
        }
    }
}
