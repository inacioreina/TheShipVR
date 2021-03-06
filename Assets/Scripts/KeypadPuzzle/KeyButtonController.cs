using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(InteractableController))]
public class KeyButtonController : MonoBehaviour
{
    private string keyButtonID;

    delegate void InputCode(string key);
    InputCode KeyButtonPress;



    private void Awake()
    {
        SetKeyButtonID();

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


    //when player presses the key
    public void ButtonPress()
    {
        KeyButtonPress.Invoke(keyButtonID);
    }
}
