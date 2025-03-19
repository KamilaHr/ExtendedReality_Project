using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    private bool isUnlocked = false; 
    public Vector3 openPosition; 
    private Vector3 closedPosition; 
    public float doorSpeed = 2f;

    private void Start()
    {
        closedPosition = transform.position;
    }

    public void UnlockDoor()
    {
        isUnlocked = true;
    }

    private void Update()
    {
        if (isUnlocked)
        {
            transform.position = Vector3.Lerp(transform.position, openPosition, Time.deltaTime * doorSpeed);
        }
    }

}
