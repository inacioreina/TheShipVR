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
    }


    public void GrabPlanet()
    {
        transform.SetParent(Camera.main.transform);

    }

    public void DropPlanet()
    {
        transform.SetParent(null);
        EnableDisableRigidbody(true);
    }

    private void EnableDisableRigidbody(bool enableRigidbody)
    {
        planetRigidbody.isKinematic = !enableRigidbody;
        planetRigidbody.detectCollisions = enableRigidbody;
        planetCollider.enabled = enableRigidbody;
    }


    public IEnumerator InsertPlanetMotion()
    {
        float time = 0f;

        float duration = 2f;

        Vector3 initialPosition = transform.localPosition;

        Quaternion initialRotation = transform.localRotation;

        transform.SetParent(transform);

        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(initialPosition, Vector3.zero, time / duration);

            transform.localRotation = Quaternion.Lerp(initialRotation, Quaternion.identity, Mathf.Clamp01(time * 2 / duration));

            time += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        yield return new WaitForSeconds(1f);
    }
}
