using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightAudio : MonoBehaviour
{
    public AudioSource flashlightOn, pumpFlashlight;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightOn.Play();
        }

        if (Input.GetKey(KeyCode.R))
        {
            pumpFlashlight.enabled = true;
        }
        else
        {
            pumpFlashlight.enabled = false;
        }
    }
}
