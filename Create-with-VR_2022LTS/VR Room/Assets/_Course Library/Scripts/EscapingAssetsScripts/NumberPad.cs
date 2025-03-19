using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberPad : MonoBehaviour
{
    public TextMeshProUGUI InputDisplayText; 
    public KeycardSpawner CardSpawner; 
    private string correctCode = "1234";
    private string enteredCode = "";


    private void Start()
    {
        
    }
    public void ButtonPressed(int valuePressed)
    {
        Debug.Log("Button Pressed: " + valuePressed);

        if (enteredCode.Length < correctCode.Length)
        {
            enteredCode += valuePressed.ToString();
            Debug.Log("Current Entered Code: " + enteredCode); // Debugging
            InputDisplayText.text = enteredCode;
        }

        if (enteredCode.Length == correctCode.Length)
        {
            if (enteredCode == correctCode)
            {
                Debug.Log("✅ Correct Code Entered! Spawning keycard...");
                CardSpawner.SpawnKeycard();
            }
            else
            {
                Debug.Log("❌ Incorrect Code! Resetting...");
                enteredCode = "";
                InputDisplayText.text = "";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
