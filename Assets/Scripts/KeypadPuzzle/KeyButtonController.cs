using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyButtonController : MonoBehaviour
{
    private string keyButtonID;

    delegate void InputCode(string key);
    InputCode KeyButtonPress;



    private void Start()
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
        keyButtonID = gameObject.name.Replace("Key", string.Empty);

        Debug.Log($"{gameObject.name} ID: {keyButtonID}");
    }


    //when player presses the key
    public void ButtonPress()
    {
        KeyButtonPress.Invoke(keyButtonID);
    }
}
