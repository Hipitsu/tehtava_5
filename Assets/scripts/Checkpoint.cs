using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber;
    public bool isActiveCheckpoint;

    private Light passLight;
    private RaceController raceController;
    
    void Start()
    {
        passLight = GetComponentInChildren<Light>();
        raceController = GetComponentInParent<RaceController>();
        
    } // Start

    private void Update()
    {
        if (isActiveCheckpoint)
        {
            passLight.intensity = 4 + Mathf.Sin(Time.time * 2) * 2;
            passLight.color = Color.red;
        }
        else
        {
            passLight.intensity -= 0.1f; 
        }
    } // Update

    private void OnTriggerEnter(Collider other)
    {
        if (isActiveCheckpoint && other.name == "PlayerShip")
        {
            passLight.intensity = 8.0f;
            passLight.color = Color.green;
            raceController.CheckpointPassed();
            isActiveCheckpoint = false;
        }
    } //OnTriggerEnter

} // Class
