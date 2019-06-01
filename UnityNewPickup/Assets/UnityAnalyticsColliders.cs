using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class UnityAnalyticsColliders : MonoBehaviour
{
    public string colliderName;
    public bool doDisable = true;
    private bool disabled = false;

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && !disabled)
        {
            Analytics.CustomEvent(colliderName);

            if (doDisable)
            {
                disabled = true;
            }
        }
    }
}