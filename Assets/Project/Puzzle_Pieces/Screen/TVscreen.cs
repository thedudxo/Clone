using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVscreen : MonoBehaviour {

    [SerializeField] GameObject[] cubes;

    public void displayCubes(int ammount)
    {
        
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
}
