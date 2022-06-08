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

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;

    /// <summary>Game object that is being gazed.</summary>
    private InteractableController gazedObject = null;


    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            GameObject hitObject = hit.transform.gameObject;

            hitObject.TryGetComponent(out InteractableController interactable);

            // Interactable GameObject detected in front of the camera.
            if (gazedObject != interactable)
            {
                // New GameObject.
                gazedObject?.OnGazeExit();
                gazedObject = interactable;
                gazedObject.OnGazeEnter();
                Debug.Log($"New Gazed Object: {gazedObject.gameObject.name}");
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            gazedObject?.OnGazeExit();
            gazedObject = null;            
        }

        // Checks for screen touches.
        if (Api.IsTriggerPressed)
        {
            gazedObject?.OnInteraction();
            Debug.Log("Trigger Pressed");
        }
    }
}
