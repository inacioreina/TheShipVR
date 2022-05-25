using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KeypadController : MonoBehaviour
{
    /// <summary>Code that is the answer to the puzzle.</summary> 
    [SerializeField]
    private string answerCode = string.Empty;

    /// <summary>Code that the player inputs into the keypad.</summary>
    private string inputCode = string.Empty;


    public void GetKeyPress(string key)
    {
        switch (key)
        {
            case "Submit": //enter code
                SubmitCode();
                break;
            case "Delete": //delete code
                DeleteCode();
                break;
            default: //input number
                AddNumberToCode(key);
                break;
        }
    }

    private void SubmitCode()
    {
        //correct
        if (answerCode == inputCode)
        {
            Debug.Log("Correct!");
        }
        else //incorrect
        {
            Debug.Log("Incorrect!");
        }
    }

    private void DeleteCode()
    {
        inputCode = string.Empty;
        Debug.Log("Code Deleted.");
    }

    private void AddNumberToCode(string key)
    {
        if (inputCode.Length < 4)
        {
            inputCode += key;
            Debug.Log($"Code: {inputCode}");
        }
    }
}
