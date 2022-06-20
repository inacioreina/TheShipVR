using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
[RequireComponent(typeof(Rigidbody))]
public class PlanetController : MonoBehaviour
{
    [SerializeField]
    private Planet planetType = Planet.Sun;

    private Rigidbody planetRigidbody;

    private BoxCollider planetCollider;

    private InteractableController interactable;

    

    public Planet PlanetType
    {
        get
        {
            return planetType;
        }
    }

    private void Start()
    {
        planetRigidbody = GetComponent<Rigidbody>();
        planetCollider = GetComponent<BoxCollider>();
        interactable = GetComponent<InteractableController>();
    }


    public void GrabPlanet()
    {
        transform.SetParent(Camera.main.transform, false);
        EnableDisableRigidbody(false);
        transform.localPosition = new Vector3(0.3f, -0.1f, 0.5f);
        transform.localRotation = Quaternion.Euler(90, 0, 0);
    }

    public void DropPlanet()
    {
        transform.SetParent(null, true);
        transform.localRotation = Quaternion.identity;
        EnableDisableRigidbody(true);
    }

    private void EnableDisableRigidbody(bool enableRigidbody)
    {
        planetRigidbody.isKinematic = !enableRigidbody;
        planetRigidbody.detectCollisions = enableRigidbody;
        planetCollider.enabled = enableRigidbody;
    }
}
