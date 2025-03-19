using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycardreader : MonoBehaviour
{

    public SlidingDoor Door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Keycard"))
        {
            Debug.Log("? Keycard Accepted! Unlocking Door...");
            Door.UnlockDoor();
            Destroy(other.gameObject); // Remove the keycard
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
