//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using Google.XR.Cardboard;

enum ActionType
{
    None,
    Teleport,
    Interaction
}

public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;

    private Transform player;

    private GameObject pointer;

    private ActionType currentAction = ActionType.None;

    /// <summary> Pointer to display teleport target. </summary>
    private Vector3 pointerPosition = Vector3.zero;

    /// <summary>Game object that is being gazed.</summary>
    private InteractableController gazedObject = null;


    private void Awake()
    {
        player = transform.parent;

        pointer = player.GetChild(1).gameObject;
    }

    private void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.TryGetComponent(out InteractableController interactable))
            {
                // Interactable GameObject detected in front of the camera.
                if (gazedObject != interactable)
                {
                    // New GameObject.
                    gazedObject?.OnGazeExit();
                    gazedObject = interactable;
                    gazedObject.OnGazeEnter();
                    DisplayTeleportPointer(false);
                    currentAction = ActionType.Interaction;
                    Debug.Log($"New Gazed Object: {gazedObject.gameObject.name}");
                }
            }
            else if (hitObject.CompareTag("TeleportFloor"))
            {
                // Ground detected
                RemoveGazedObject();
                pointerPosition = hit.point;
                DisplayTeleportPointer(true);
                currentAction = ActionType.Teleport;
                Debug.Log("Ground detected");
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            RemoveGazedObject();
            DisplayTeleportPointer(false);
            currentAction = ActionType.None;
        }

        // Checks for screen touches.
        if (Api.IsTriggerPressed)
        {
            switch (currentAction)
            {
                case ActionType.None:
                    break;
                case ActionType.Teleport:
                    TeleportPlayer();
                    break;
                case ActionType.Interaction:
                    gazedObject?.OnInteraction();
                    break;
                default:
                    break;
            }
            
            Debug.Log("Trigger Pressed");
        }
    }

    private void RemoveGazedObject()
    {
        gazedObject?.OnGazeExit();
        gazedObject = null;
    }


    private void DisplayTeleportPointer(bool hit)
    {
        if (hit)
        {
            pointer.SetActive(true);
            pointer.transform.position = pointerPosition;
        }
        else
        {
            pointer.SetActive(false);
        }
    }

    private void TeleportPlayer()
    {
        player.position = new Vector3(pointerPosition.x, player.position.y, pointerPosition.z);
    }
}
