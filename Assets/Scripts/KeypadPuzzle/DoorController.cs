using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Transform door;

    [SerializeField]
    private float openFinalPosition;

    [SerializeField]
    private float closeFinalPosition;

    [SerializeField]
    private float openCloseDuration;

    private void Start()
    {
        door = transform.GetChild(1);
    }


    public void OpenDoor()
    {
        StartCoroutine(MoveDoor(openFinalPosition, openCloseDuration));
    }

    public void CloseDoor()
    {
        StartCoroutine(MoveDoor(closeFinalPosition, openCloseDuration));
    }

    IEnumerator MoveDoor(float finalY, float duration)
    {
        float time = 0f;

        float initialY = door.position.y;

        float doorY;

        while (time < duration)
        {
            doorY = Mathf.Lerp(initialY, finalY, time/duration);

            door.position = new Vector3(door.position.x, doorY, door.position.z);

            yield return null;

            time += Time.deltaTime;
        }

        doorY = finalY;

        door.position = new Vector3(door.position.x, doorY, door.position.z);
    }
}
