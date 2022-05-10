using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//depois coloco comentarios

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    private float mouseSensitivity = 15f;

    [SerializeField]
    private float verticalViewClamp = 80f;

    private CharacterController player;
    private InputManager inputManager;
    private Camera mainCamera;
    private Transform cameraPivot;
    private float horizontalView;
    private float verticalView;

    private Vector2 inputVector;
    private Vector3 movement;
    private float cameraPivotRotationX;

    private void Start()
    {
        player = GetComponent<CharacterController>();
        inputManager = InputManager.InputInstance;
        mainCamera = Camera.main;
        cameraPivot = GameObject.FindGameObjectWithTag("CameraPivot").transform;
    }

    void Update()
    {
        inputVector = inputManager.GetPlayerMovement();
        movement = mainCamera.transform.forward.normalized * inputVector.y + mainCamera.transform.right.normalized * inputVector.x;

        player.Move(movement * Time.deltaTime * speed);

        Vector3 position = transform.position;
        position.y = 1f;
        transform.position = position;

        horizontalView = inputManager.GetPlayerLook().x * mouseSensitivity * Time.deltaTime;
        verticalView = inputManager.GetPlayerLook().y * mouseSensitivity * Time.deltaTime;
    }

    private void LateUpdate()
    {
        Debug.Log($"Horizontal View = {horizontalView} | Vertical View = {verticalView}");

        cameraPivotRotationX -= verticalView;
        cameraPivotRotationX = Mathf.Clamp(cameraPivotRotationX, -verticalViewClamp, verticalViewClamp);
        cameraPivot.localRotation = Quaternion.Euler(cameraPivotRotationX, 0f, 0f);

        transform.Rotate(Vector3.up * horizontalView);
    }
}
