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

    [SerializeField]
    private Material UnselectedMaterial;

    private MeshRenderer meshRenderer;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }


    public void OnGazeEnter()
    {
        meshRenderer.material = selectedMaterial;
    }

    public void OnGazeExit()
    {
        meshRenderer.material = UnselectedMaterial;
    }

    public void OnInteraction()
    {
        switch (interactableType)
        {
            case InteractableType.KeypadButton:
                gameObject.GetComponent<KeyButtonController>().ButtonPress();
                break;
            case InteractableType.Planet:
                break;
            case InteractableType.PlanetButton:
                break;
            case InteractableType.Enemy:
                break;
            default:
                break;
        }

        Debug.Log($"Game object {gameObject.name} interacted.");
    }
}
