﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {
    public Vector3 rotationSpeed;
void Update () {
    gameObject.transform.eulerAngles += rotationSpeed;
    }
}
