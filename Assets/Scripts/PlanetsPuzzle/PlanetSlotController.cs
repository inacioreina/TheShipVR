using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]

public class PlanetSlotController : MonoBehaviour
{
    [SerializeField]
    private Planet planetSlotType = Planet.Sun;

    private BoxCollider planetSlotCollider;

    private PlanetsPuzzleManager manager;

    private void Start()
    {
        planetSlotCollider = gameObject.GetComponent<BoxCollider>();

        manager = transform.GetComponentInParent<PlanetsPuzzleManager>();
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

        yield return StartCoroutine(InsertPlanetMotion(planet));

        manager.UpdatePlanetsPuzzleProgress(planetSlotType);

        planetSlotCollider.enabled = false;
    }

    //insert planet animation
    private IEnumerator InsertPlanetMotion(PlanetController planet)
    {
        float time = 0f;

        float duration = 1f;

        planet.transform.SetParent(transform, true);

        planet.MeshRenderer.enabled = true;

        Vector3 initialPosition = planet.transform.localPosition;

        Quaternion initialRotation = planet.transform.localRotation;

        while (time < duration)
        {
            planet.transform.localPosition = Vector3.Lerp(initialPosition, Vector3.zero, time / duration);

            planet.transform.localRotation = Quaternion.Lerp(initialRotation, Quaternion.identity, Mathf.Clamp01(time * 2 / duration));

            time += Time.deltaTime;

            yield return null;
        }

        planet.transform.localPosition = Vector3.zero;
        planet.transform.localRotation = Quaternion.identity;

        yield return new WaitForSeconds(1f);
    }
}
