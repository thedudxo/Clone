using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytestDoor : MonoBehaviour
{
    [SerializeField] private BeamButton[] buttons;
    [SerializeField] private int cubesRequired; //set to a negative number for regular buttonage
    [HideInInspector]
    public GameObject childDoor;
    void Start ()
    {
        childDoor = gameObject.transform.GetChild(0).gameObject;
        childDoor.SetActive(true);       
    }

    void Update ()
    {
        int buttonsPressed = 0;
          
        foreach (BeamButton button in buttons)
        {
            Debug.Log("cubes on Button" + button.cubes);
            if (button.cubes == cubesRequired)
            {
                buttonsPressed++;
            }
        }

        if (buttonsPressed >= buttons.Length)
        {
            childDoor.SetActive(false);
        }

        else
        {
            childDoor.SetActive(true);
        }
    }
}
