using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private static InputManager inputInstance;

    public static InputManager InputInstance
    {
        get
        {
            return inputInstance;
        }
    }

    private void Awake()
    {
        if (inputInstance != null && inputInstance != this)
        {
            Destroy(this);
        }
        else
        {
            inputInstance = this;
        }

        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerInput.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerLook()
    {
        return playerInput.Player.Look.ReadValue<Vector2>();
    }

    public bool GetPlayerInteract()
    {
        return playerInput.Player.Interact.triggered;
    }
}
