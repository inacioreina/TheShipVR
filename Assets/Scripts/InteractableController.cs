using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
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
        Debug.Log("Game object {gameobject.name} interacted.");
    }
}
