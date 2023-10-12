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
    // Start is called before the first frame update

    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.rotation = camera.rotation;
        if (Input.GetKeyDown(KeyCode.F))
        {
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
                PumpFlashlight(0.03f);
            }
        }


    }
    private void PumpFlashlight(float amount)
    {
        light.intensity += amount;
    }
    
}
