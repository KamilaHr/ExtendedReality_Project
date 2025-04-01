using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlidingDoorFunctionality : MonoBehaviour
{
    [Header("Sliding Settings")]
    public float minZ = 0.3204f;
    public float maxZ = 0.8204f;

    [Header("Unlock Settings")]
    public bool isUnlocked = false;
    public XRGrabInteractable grabInteractable;

    private float initialGrabHandZ;
    private float initialDoorZ;
    private bool isBeingGrabbed = false;

    private void Start()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
            grabInteractable.enabled = false;
        }
    }

    private void Update()
    {
        if (!isBeingGrabbed || grabInteractable.interactorsSelecting.Count == 0)
            return;

        Transform interactor = grabInteractable.interactorsSelecting[0].transform;
        Debug.Log("Interactor Z: " + interactor.position.z);


        float currentHandZ = interactor.position.z;

        // Calculate how far the hand has moved since grab
        float handDeltaZ = currentHandZ - initialGrabHandZ;

        // Move the door only in +Z direction
        float newZ = Mathf.Clamp(initialDoorZ + handDeltaZ, minZ, maxZ);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!isUnlocked) return;

        isBeingGrabbed = true;
        Transform interactor = args.interactorObject.transform;
        initialGrabHandZ = interactor.position.z;
        initialDoorZ = transform.position.z;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isBeingGrabbed = false;
        initialDoorZ = transform.position.z;
        initialGrabHandZ = 0f;
    }

    public void UnlockDoor()
    {
        isUnlocked = true;
        if (grabInteractable != null)
            grabInteractable.enabled = true;
    }
}
