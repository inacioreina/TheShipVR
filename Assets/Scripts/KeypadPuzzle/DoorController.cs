using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
public class DoorController : MonoBehaviour
{
    private Transform door;

    private AudioSource doorOpenAudio;

    private BoxCollider doorCollider;

    [SerializeField]
    private float openFinalPosition;

    [SerializeField]
    private float closeFinalPosition;

    [SerializeField]
    private float openCloseDuration;

    private void Start()
    {
        door = transform.GetChild(1);

        doorOpenAudio = gameObject.GetComponent<AudioSource>();

        doorCollider = gameObject.GetComponent<BoxCollider>();
    }


    public IEnumerator OpenDoor()
    {
        yield return StartCoroutine(MoveDoor(openFinalPosition, openCloseDuration));

        doorCollider.enabled = false;
    }

    public void CloseDoor()
    {
        doorCollider.enabled = true;

        StartCoroutine(MoveDoor(closeFinalPosition, openCloseDuration));
    }

    IEnumerator MoveDoor(float finalY, float duration)
    {
        float time = 0f;

        float initialY = door.position.y;

        float doorY;

        doorOpenAudio.Play();

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
