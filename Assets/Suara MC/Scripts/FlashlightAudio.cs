using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightAudio : MonoBehaviour
{
    public AudioSource pumpFlashlight;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.Instance.PlaySFX("Flashlight", transform.position);
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
