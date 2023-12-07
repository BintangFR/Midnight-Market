using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Transform camera;
    public Light light;
    private bool drainOverTime = true;

    public float minBrightness;

    public float maxBrightness;

    public float drainRate;
    private AudioSource pumpFlashlight;

    // Start is called before the first frame update

    void Start()
    {
        light = GetComponent<Light>();
        pumpFlashlight = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.rotation;

        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.Instance.PlaySFX("Flashlight", transform.position);
            light.enabled = !light.enabled;
        }

        Mathf.Clamp(light.intensity, minBrightness, maxBrightness);
        if (drainOverTime && light.enabled)
        {
            if (light.intensity > minBrightness)
            {
                light.intensity -= Time.deltaTime * (drainRate/1000);
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (light.intensity < maxBrightness)
            {
                PumpFlashlight(10 * Time.deltaTime);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            pumpFlashlight.Play();
            AIController.Instance.ApproachPlayerSound(transform.position);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            pumpFlashlight.Stop();
        }
    }
    private void PumpFlashlight(float amount)
    {
        light.intensity += amount;
    }
    
    public void AutoOnFlashlight()
    {
        StartCoroutine(AutoOn());
    }

    private IEnumerator AutoOn()
    {
        yield return new WaitForSeconds(3);
        AudioManager.Instance.PlaySFX("Flashlight", transform.position);
        light.enabled = true;
    }
}
