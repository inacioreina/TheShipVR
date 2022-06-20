using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    KeypadButton,
    Planet,
    PlanetSlot,
    Door
}

[RequireComponent(typeof(BoxCollider))]
public class InteractableController : MonoBehaviour
{
    [SerializeField]
    private InteractableType interactableType;

    [SerializeField]
    private Material selectedMaterial;

    private Material unselectedMaterial;

    private MeshRenderer meshRenderer;


    public InteractableType InteractableType
    {
        get
        {
            return interactableType;
        }
    }


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        unselectedMaterial = meshRenderer.material;
    }


    public void OnGazeEnter()
    {
        switch (interactableType)
        {
            case InteractableType.PlanetSlot:

                Material[] slotMats = meshRenderer.materials;

                slotMats[0] = selectedMaterial;
                slotMats[1] = selectedMaterial;

                meshRenderer.materials = slotMats;
                break;
            case InteractableType.Door:
                Material[] doorMats = meshRenderer.materials;

                doorMats[0] = selectedMaterial;
                doorMats[1] = selectedMaterial;
                doorMats[1] = selectedMaterial;

                meshRenderer.materials = doorMats;
                break;
            default:
                meshRenderer.material = selectedMaterial;
                break;
        }
    }

    public void OnGazeExit()
    {
        switch (interactableType)
        {
            case InteractableType.PlanetSlot:
                Material[] slotMats = meshRenderer.materials;

                slotMats[0] = unselectedMaterial;
                slotMats[1] = unselectedMaterial;

                meshRenderer.materials = slotMats;
                break;
            case InteractableType.Door:
                Material[] doorMats = meshRenderer.materials;

                doorMats[0] = unselectedMaterial;
                doorMats[1] = unselectedMaterial;
                doorMats[1] = unselectedMaterial;

                meshRenderer.materials = doorMats;
                break;
            default:
                meshRenderer.material = unselectedMaterial;
                break;
        }
    }

    public void OnInteraction(PlanetController holdingPlanet = null)
    {
        switch (interactableType)
        {
            case InteractableType.KeypadButton:
                gameObject.GetComponent<KeyButtonController>().ButtonPress();
                break;
            case InteractableType.PlanetSlot: //insert planet in slot if it matches
                if (holdingPlanet != null)
                {
                    gameObject.TryGetComponent(out PlanetSlotController planetSlot);

                    if (planetSlot.CompareDiscSlot(holdingPlanet))
                    {
                        Camera.main.GetComponent<CameraPointer>().InsertPlanet();
                        StartCoroutine(planetSlot.InsertPlanet(holdingPlanet));
                    }
                }
                break;
            case InteractableType.Door:
                gameObject.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(transform.GetComponentInParent<DoorController>().OpenDoor());
                break;
            default:
                break;
        }

        Debug.Log($"Game object {gameObject.name} interacted.");
    }
}
