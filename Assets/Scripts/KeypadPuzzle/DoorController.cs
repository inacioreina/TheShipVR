using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Transform door;

    private void Start()
    {
        door = transform.GetChild(1);
    }


    public void OpenDoor()
    {
        StartCoroutine(MoveDoor(2.09f, 2.5f));
    }

    public void CloseDoor()
    {
        StartCoroutine(MoveDoor(2.09f, 2.5f));
    }

    IEnumerator MoveDoor(float finalY, float duration)
    {
        float time = 0f;

        float doorY = door.position.y;

        float initialY = doorY;

        while (time < duration)
        {
            doorY = Mathf.Lerp(initialY, finalY, time/duration);

            door.position = new Vector3(door.position.x, doorY, door.position.z);

            yield return null;

            time += Time.deltaTime;
        }
    }
}
