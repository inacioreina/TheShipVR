using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]

public class PlanetSlotController : MonoBehaviour
{
    [SerializeField]
    private Planet planetSlotType = Planet.Sun;

    private PlanetsPuzzleManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("PlanetsPuzzle").GetComponent<PlanetsPuzzleManager>();
    }

    //verify if planet disc fits the planet slot
    public bool CompareDiscSlot(PlanetController planet)
    {
        return planetSlotType == planet.PlanetType;
    }

    //insert planet if above function is true
    public IEnumerator InsertPlanet(PlanetController planet)
    {
        planet.GetComponentInParent<CameraPointer>().StopHoldingPlanet();

        yield return StartCoroutine(planet.InsertPlanetMotion());

        manager.UpdatePlanetsPuzzleProgress(planetSlotType);
    }
}
