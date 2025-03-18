using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PictureSwap : MonoBehaviour
{
    public XRSocketInteractor socket;

    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (socket.selectTarget != null)
        {
            IXRSelectInteractable existingObject = socket.selectTarget;
            GameObject existingObjectGameObject = existingObject.transform.gameObject;

            IXRSelectInteractable newObject = other.GetComponent<IXRSelectInteractable>();
            GameObject newObjectGameObject = newObject.transform.gameObject;

            if (newObject != null)
            {
                // Store the previous position of the new object before swapping
                Vector3 newObjectPreviousPosition = newObjectGameObject.transform.position;

                // Remove the existing object from the socket
                socket.interactionManager.SelectExit(socket, existingObject);

                // Move the old object to the previous position of the new object
                existingObjectGameObject.transform.position = newObjectPreviousPosition;

                // Place the new object into the socket
                socket.interactionManager.SelectEnter(socket, newObject);
            }
        }
    }
}

