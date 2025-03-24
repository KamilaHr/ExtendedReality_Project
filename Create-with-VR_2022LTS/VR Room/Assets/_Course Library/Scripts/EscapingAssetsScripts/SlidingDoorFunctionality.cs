using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlidingDoorFunctionality : MonoBehaviour
{
    [Header("Sliding Settings")]
    public float minZ = 0f;
    public float maxZ = 2f;
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

        // Calculate hand movement along Z
        Transform interactor = grabInteractable.interactorsSelecting[0].transform;
        float deltaZ = interactor.position.z - lastHandPosition.z;
        lastHandPosition = interactor.position;

        // Move door along Z only
        float newZ = Mathf.Clamp(transform.position.z + deltaZ, minZ, maxZ);
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, newZ);

        // Smooth movement
        transform.position = targetPosition;
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
