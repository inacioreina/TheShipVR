using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Planet
{
    Sun, //yes sun is not a planet I know
    Mercury,
    Venus,
    Earth,
    Mars,
    Jupiter,
    Saturn,
    Neptune,
    Uranus
}

public class PlanetsPuzzleManager : MonoBehaviour
{
    /// <summary>Flag that indicates if puzzle is complete.</summary>
    private bool planetsPuzzleComplete;

    /// <summary>Array that represents the planets placed on the slots.</summary>
    private bool[] planetsProgress = new bool[9];


    private void Start()
    {
        InitializePlanetsPuzzle();
    }


    //setup puzzle
    private void InitializePlanetsPuzzle()
    {
        planetsPuzzleComplete = false;

        for (int i = 0; i < planetsProgress.Length; i++)
        {
            planetsProgress[i] = false;
        }
    }


    //check if puzzle is complete and if so open the door
    public void UpdatePlanetsPuzzleProgress(Planet planet)
    {
        planetsProgress[(int)planet] = true;

        foreach (bool complete in planetsProgress)
        {
            planetsPuzzleComplete = true;

            if (!complete)
            {
                planetsPuzzleComplete = false;
                break;
            }
        }

        if (planetsPuzzleComplete)
        {
            GameObject.FindGameObjectWithTag("PlanetsDoor").GetComponent<DoorController>().OpenDoor();
        }
    }
}
