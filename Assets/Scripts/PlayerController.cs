using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    private CharacterController player;
    private InputManager inputManager;
    private Vector2 inputVector;
    private Vector3 movement;
    

    private void Start()
    {
        player = GetComponent<CharacterController>();
        inputManager = InputManager.InputInstance;
    }

    void Update()
    {
        inputVector = inputManager.GetPlayerMovement();
        movement = new Vector3(inputVector.x, 0, inputVector.y);

        player.Move(movement * Time.deltaTime * speed);

        if (movement != Vector3.zero)
        {
            gameObject.transform.forward = movement;
        }
    }
}
