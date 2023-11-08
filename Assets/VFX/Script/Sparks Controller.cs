using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksController : MonoBehaviour
{
    public GameObject mascotSparks;  // Reference to the electrical sparks game object
    private LightController lightController; // Reference to the LightController

    public event Action OnSparksOn;
    public event Action OnSparksOff;
    private bool isSparksOn;

    public void Start()
    {
        mascotSparks.SetActive(false);
        lightController = LightController.Instance; // Assuming LightController is a singleton
    }

    private void Update()
    {
            if (lightController != null && lightController.isLightingOn == false)
            {
                mascotSparks.SetActive(true);
            }
    }
}