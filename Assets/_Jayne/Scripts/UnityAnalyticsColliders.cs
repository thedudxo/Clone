using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class UnityAnalyticsColliders : MonoBehaviour
{
    public string colliderName;

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Analytics.CustomEvent(colliderName);
        }
    }
}
