using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DynamicHaptics : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null || grabInteractable.interactorsSelecting.Count == 0) return;
        // Get the controller holding the object
        IXRSelectInteractor interactor = grabInteractable.interactorsSelecting[0];
        if (interactor is XRBaseControllerInteractor controllerInteractor)
        {
            // Get the impact force
            float force = collision.relativeVelocity.magnitude;

            // Map force to intensity (clamped between 0.1 and 1)
            float intensity = Mathf.Clamp(force / 5f, 0.1f, 1f);
            float duration = intensity * 0.1f;

            // Send haptic feedback
            controllerInteractor.SendHapticImpulse(intensity, duration);

            Debug.Log($"Hit force: {force} → Haptic Intensity: {intensity}");
        }
    }
}

