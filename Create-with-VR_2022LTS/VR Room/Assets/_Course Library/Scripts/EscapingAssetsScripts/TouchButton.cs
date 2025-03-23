using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TouchButton : XRBaseInteractable
{
    public int ButtonNumber;
    public Material TouchedMaterial;
    public Material NormalMaterial;
    private Renderer buttonRenderer; 
    public NumberPad LinkedKeypad;

    protected override void Awake()
    {
        base.Awake();
        buttonRenderer = GetComponent<Renderer>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        buttonRenderer.material = TouchedMaterial;
        LinkedKeypad.ButtonPressed(ButtonNumber);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        buttonRenderer.material = NormalMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
