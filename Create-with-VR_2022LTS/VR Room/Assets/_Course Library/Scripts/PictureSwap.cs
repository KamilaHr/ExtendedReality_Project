using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PictureSwap : MonoBehaviour
{
    private XRSocketInteractor socket;

    // Start is called before the first frame update
    private void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure the entering object is a valid interactable
        IXRSelectInteractable newObject = other.GetComponent<IXRSelectInteractable>();
        if (newObject == null) return;

        // Check if the socket already has an object
        if (socket.selectTarget != null)
        {
            // Get the current object in the socket
            IXRSelectInteractable existingObject = socket.selectTarget;
            GameObject existingObjectGameObject = existingObject.transform.gameObject;
            GameObject newObjectGameObject = newObject.transform.gameObject;

            // Store the previous position of the new object before swapping
            Vector3 newObjectPreviousPosition = newObjectGameObject.transform.position;

            // Disable gravity and enable kinematic mode for smooth placement
            Rigidbody existingRb = existingObjectGameObject.GetComponent<Rigidbody>();
            Rigidbody newRb = newObjectGameObject.GetComponent<Rigidbody>();

            existingRb.useGravity = false;
            existingRb.isKinematic = true;

            newRb.useGravity = false;
            newRb.isKinematic = true;

            // Remove the existing object from the socket
            socket.interactionManager.SelectExit(socket, existingObject);

            // Move the old object to the previous position of the new object
            existingObjectGameObject.transform.position = newObjectPreviousPosition;

            // Place the new object into the socket
            socket.interactionManager.SelectEnter(socket, newObject);
        }
    }
}