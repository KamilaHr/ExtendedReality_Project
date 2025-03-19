using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardSpawner : MonoBehaviour
{

    public GameObject KeycardPrefab; 
    public Transform SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnKeycard()
    {
        if (KeycardPrefab != null && SpawnPoint != null)
        {
            Instantiate(KeycardPrefab, SpawnPoint.position, SpawnPoint.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
