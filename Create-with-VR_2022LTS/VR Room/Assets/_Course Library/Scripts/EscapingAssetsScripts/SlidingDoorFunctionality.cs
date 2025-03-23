using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlidingDoorFunctionality : MonoBehaviour
{
    [Header("Sliding Settings")]
    public float minX = 0f;
    public float maxX = 2f;
    public float smoothness = 10f;

    [Header("Unlock Settings")]
    public bool isUnlocked = false;
    public XRGrabInteractable grabInteractable;

    private Vector3 lastHandPosition;
    private bool isBeingGrabbed = false;

    private void Start()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
            grabInteractable.enabled = false; // Locked at start
        }
    }

    private void Update()
    {
        if (!isBeingGrabbed) return;

        // Calculate hand movement along X
        Transform interactor = grabInteractable.interactorsSelecting[0].transform;
        float deltaX = interactor.position.x - lastHandPosition.x;
        lastHandPosition = interactor.position;

        // Move door along X
        Vector3 newPosition = transform.position + new Vector3(deltaX, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        Vector3 clamped = new Vector3(Mathf.Clamp(newPosition.z, minX, maxX),
            transform.position.y,
            transform.position.x);
        transform.position = Vector3.Lerp(transform.position, clamped, Time.deltaTime * smoothness);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!isUnlocked) return;

        isBeingGrabbed = true;
        lastHandPosition = args.interactorObject.transform.position;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isBeingGrabbed = false;
    }

    public void UnlockDoor()
    {
        isUnlocked = true;
        if (grabInteractable != null)
            grabInteractable.enabled = true;
    }
}
