using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyButtonController : MonoBehaviour
{
    private string keyButtonID;

    private MeshRenderer keyMeshRenderer;

    delegate void InputCode(string key);
    InputCode KeyButtonPress;



    private void Awake()
    {
        SetKeyButtonID();

        SetMeshRenderer();

        SetDelegates();
    }


    private void SetDelegates()
    {
        KeypadController keypadController = transform.parent.GetComponent<KeypadController>();

        KeyButtonPress = keypadController.GetKeyPress;

        Debug.Log($"{gameObject.name} connected to {keypadController.name}");
    }


    //set the key ID based on gameobject name
    private void SetKeyButtonID()
    {
        keyButtonID = gameObject.name;

        Debug.Log($"Key {gameObject.name} ID: {keyButtonID}");
    }

    private void SetMeshRenderer()
    {
        keyMeshRenderer = GetComponent<MeshRenderer>();
    }


    public void ChangeKeyMaterial(Material material)
    {
        keyMeshRenderer.materials[0] = material;
    }


    //when player presses the key
    public void ButtonPress()
    {
        KeyButtonPress.Invoke(keyButtonID);
    }
}
