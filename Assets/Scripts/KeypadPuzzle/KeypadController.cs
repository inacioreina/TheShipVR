using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class KeypadController : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro inputCodeUI;

    ///<summary>Code that is the answer to the puzzle.</summary> 
    private string answerCode = "4236";

    ///<summary>Code that the player inputs into the keypad.</summary>
    private string inputCode = string.Empty;

    ///<summary>Flag to prevent interaction with the keypad after puzzle complete</summary>
    private bool puzzleClear = false;


    private void Start()
    {
        DeleteCode();
    }


    public void GetKeyPress(string key)
    {
        //if puzzle isn't clear yet get input
        if (!puzzleClear)
        {
            switch (key)
            {
                case "Delete": //delete code
                    DeleteCode();
                    break;
                default: //input number
                    AddNumberToCode(key);
                    break;
            }
        }
    }

    private void DeleteCode()
    {
        inputCode = string.Empty;

        UpdateCodeUI();

        Debug.Log("Code Deleted.");
    }

    private void AddNumberToCode(string key)
    {
        inputCode += key;

        UpdateCodeUI();
        
        //submit code if length is same length of answer code
        if (inputCode.Length == answerCode.Length)
        {
            SubmitCode();
        }
    }

    private void SubmitCode()
    {
        //correct
        if (answerCode == inputCode)
        {
            puzzleClear = true;

            Debug.Log("Correct!");

            GameObject.FindGameObjectWithTag("KeypadDoor").GetComponent<DoorController>().OpenDoor();
        }
        else //incorrect
        {
            Debug.Log("Incorrect!");
            DeleteCode();
        }
    }

    private void UpdateCodeUI()
    {
        Debug.Log($"Code: {inputCode}");

        inputCodeUI.text = inputCode;
    }
}
