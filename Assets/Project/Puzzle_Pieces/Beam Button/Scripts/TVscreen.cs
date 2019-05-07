using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVscreen : MonoBehaviour {

    [SerializeField] GameObject[] cubes;
    [SerializeField] float randomRotation;

    public void displayCubes(int ammount)
    {
        Debug.Log("yehs");
        int i = 1;
        foreach (GameObject cube in cubes)
        {
            if(i <= ammount)
            {
                cube.SetActive(true);
            }
            else
            {
                cube.SetActive(false);
            }
            i++;
        }
    }

    public void Update()
    {
        foreach (GameObject cube in cubes)
        {
            cube.GetComponent<Rigidbody>().AddTorque(
                Random.Range(-randomRotation,randomRotation),
                Random.Range(-randomRotation, randomRotation),
                Random.Range(-randomRotation, randomRotation)
                );
        }
    }
}
