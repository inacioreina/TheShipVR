using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    KeypadButton,
    Planet,
    PlanetSlot,
    Enemy
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
                meshRenderer.materials[0] = selectedMaterial;
                meshRenderer.materials[1] = selectedMaterial;
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
                meshRenderer.materials[0] = unselectedMaterial;
                meshRenderer.materials[1] = unselectedMaterial;
                break;
            default:
                meshRenderer.material = unselectedMaterial;
                break;
        }
    }

    public void OnInteraction(PlanetController planet = null)
    {
        switch (interactableType)
        {
            case InteractableType.KeypadButton:
                gameObject.GetComponent<KeyButtonController>().ButtonPress();
                break;
            case InteractableType.Planet: //grab planet
                if (planet == null)
                    planet.GrabPlanet();
                break;
            case InteractableType.PlanetSlot: //insert planet in slot if it matches
                if (planet != null)
                {
                    gameObject.TryGetComponent(out PlanetSlotController planetSlot);

                    if (planetSlot.CompareDiscSlot(planet))
                    {
                        StartCoroutine(planetSlot.InsertPlanet(planet));
                    }
                }
                    
                
                break;
            case InteractableType.Enemy:
                //write here the reference to the function that will interact with enemy
                break;
            default:
                break;
        }

        Debug.Log($"Game object {gameObject.name} interacted.");
    }
}
