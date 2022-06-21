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
    Interaction,
    Grab,
    Drop
}

public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;

    private Transform player;

    private GameObject pointer;

    private AudioSource footstepAudio;

    private ActionType currentAction = ActionType.None;

    /// <summary> Pointer to display teleport target. </summary>
    private Vector3 pointerPosition = Vector3.zero;

    /// <summary>Game object that is being gazed.</summary>
    private InteractableController gazedObject = null;

    /// <summary>Planet disc that is being held.</summary>
    private PlanetController currentHoldingPlanet = null;


    private void Awake()
    {
        player = transform.parent;

        pointer = player.GetChild(1).gameObject;

        footstepAudio = gameObject.GetComponent<AudioSource>();
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

                    //if player is holding a planet and is gazing at another planet don't let the player grab that object
                    //or if player is not holding a planet and is gazing at a slot don't let the player interact with the slot
                    if (GazingExceptions(interactable))
                    {
                        RemoveGazedObject();
                    }
                    else
                    {
                        gazedObject?.OnGazeExit();
                        gazedObject = interactable;
                        gazedObject.OnGazeEnter();
                        DisplayTeleportPointer(false);

                        switch (interactable.InteractableType)
                        {
                            case InteractableType.Planet:
                                currentAction = ActionType.Grab;
                                break;
                            default:
                                currentAction = ActionType.Interaction;
                                break;
                        }

                        
                        Debug.Log($"New Gazed Object: {gazedObject.gameObject.name}");
                    }
                    
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
            else
            {
                LookingAtNonInteractable();
            }
        }
        else
        {
            LookingAtNonInteractable();
        }

        // Checks for screen touches.
        if (Api.IsTriggerPressed || Input.GetMouseButtonDown(0))
        {
            switch (currentAction)
            {
                case ActionType.None:
                    break;
                case ActionType.Teleport:
                    TeleportPlayer();
                    break;
                case ActionType.Interaction:
                    gazedObject?.OnInteraction(currentHoldingPlanet);
                    break;
                case ActionType.Grab:
                    StartHoldingPlanet(gazedObject);
                    break;
                case ActionType.Drop:
                    StopHoldingPlanet();
                    break;
                default:
                    break;
            }
            
            Debug.Log("Trigger Pressed");
        }
    }


    private bool GazingExceptions(InteractableController interactable)
    {
        //player is looking at a planet but is holding one
        if (interactable.InteractableType == InteractableType.Planet && currentHoldingPlanet != null)
        {
            return true;
        }
        //player is looking at a planet slot but is not holding a planet
        else if (interactable.InteractableType == InteractableType.PlanetSlot && currentHoldingPlanet == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LookingAtNonInteractable()
    {
        // No GameObject detected in front of the camera.
        RemoveGazedObject();
        DisplayTeleportPointer(false);

        //if player is holding a planet allow the player to drop the object
        if (currentHoldingPlanet != null)
        {
            currentAction = ActionType.Drop;
        }
        else
        {
            currentAction = ActionType.None;
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
        footstepAudio.Play();
    }

    public void StartHoldingPlanet(InteractableController planet)
    {
        currentHoldingPlanet = planet.gameObject.GetComponent<PlanetController>();
        currentHoldingPlanet?.GrabPlanet();
    }

    public void StopHoldingPlanet()
    {
        currentHoldingPlanet?.DropPlanet();
        currentHoldingPlanet = null;
    }

    public void InsertPlanet()
    {
        currentHoldingPlanet = null;
    }
}
