using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    KeypadButton,
    Planet,
    PlanetButton,
    Enemy
}

public class InteractableController : MonoBehaviour
{
    [SerializeField]
    private InteractableType interactableType;

    [SerializeField]
    private Material selectedMaterial;

    private Material unselectedMaterial;

    private MeshRenderer meshRenderer;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        unselectedMaterial = meshRenderer.material;
    }


    public void OnGazeEnter()
    {
        meshRenderer.material = selectedMaterial;
    }

    public void OnGazeExit()
    {
        meshRenderer.material = unselectedMaterial;
    }

    public void OnInteraction()
    {
        switch (interactableType)
        {
            case InteractableType.KeypadButton:
                gameObject.GetComponent<KeyButtonController>().ButtonPress();
                break;
            case InteractableType.Planet:
                //write here the reference to the function that will interact with planet
                break;
            case InteractableType.PlanetButton:
                //write here the reference to the function that will interact with planet button
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
