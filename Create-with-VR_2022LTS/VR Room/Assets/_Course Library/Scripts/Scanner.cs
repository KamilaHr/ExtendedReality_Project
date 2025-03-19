using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class Scanner : XRGrabInteractable
{

    [Header("Scanner Data")] 
    public Animator animator; 
    public LineRenderer laserRenderer; 
    public TextMeshProUGUI targetName; 
    public TextMeshProUGUI targetPosition;

    protected override void Awake() { base.Awake(); ScannerActivated(false); }

    protected override void OnSelectEntered(SelectEnterEventArgs args) { base.OnSelectEntered(args); animator.SetBool("Opened", true); }

    protected override void OnSelectExited(SelectExitEventArgs args) { base.OnSelectExited(args); animator.SetBool("Opened", false); }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args); 
        ScannerActivated(true);

    }
    private void ScanForObjects()
    {
        RaycastHit hit; 
        Vector3 worldHit = laserRenderer.transform.position + laserRenderer.transform.forward * 1000.0f;

        if (Physics.Raycast(laserRenderer.transform.position, laserRenderer.transform.forward, out hit))
        {
            worldHit = hit.point;
            Vector3 objectSize = hit.collider.bounds.size;

            // Check if the scanned object is part of the puzzle
            if (hit.collider.CompareTag("PuzzlePiece") || hit.collider.CompareTag("PuzzleSocket"))
            {
                targetName.SetText("Puzzle Piece Detected");
                targetPosition.SetText("Put the pieces of the picture in the right order to get the full picture.");
            }

            else 
            {
                targetName.SetText("Object: " + hit.collider.name);
                targetPosition.SetText("Position: " + hit.collider.transform.position.ToString() + "\n" + "Size: " + objectSize.ToString("F2"));
            }
        }
        else
        {
            // Show "Ready to Scan" only if scanner is active
            if (laserRenderer.gameObject.activeSelf)
            {
                targetName.SetText("Ready to Scan");
                targetPosition.SetText("");
            }
        }

        laserRenderer.SetPosition(1, laserRenderer.transform.InverseTransformPoint(worldHit));
    }

    protected override void OnDeactivated(DeactivateEventArgs args) { base.OnDeactivated(args); ScannerActivated(false); }

    private void ScannerActivated(bool isActivated) 
    { 
        laserRenderer.gameObject.SetActive(isActivated); 
        targetName.gameObject.SetActive(isActivated); 
        targetPosition.gameObject.SetActive(isActivated); 
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase) 
    { 
        base.ProcessInteractable(updatePhase); 
        if (laserRenderer.gameObject.activeSelf) 
            ScanForObjects(); 
    }

    void Update() { }

} 