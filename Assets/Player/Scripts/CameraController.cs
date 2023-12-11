using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform cameraPosition;
    public Transform orientation;
    public float sens;
    private float yRotation;
    private float xRotation;
    public float minClippingDistance = 0.1f;

    private bool isShaking = false;
    private float shakeDuration = 0f;
    private float shakeStrength = 0f;

    private PlayerController playerController;


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerController.OnTakeDamage.AddListener(() => ShakeCamera(1.0f, 0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.nearClipPlane = minClippingDistance;

        //rotate camera based of sensitivity
        Camera.main.transform.position = cameraPosition.transform.position;
        float camrotx = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float camroty = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        yRotation += camrotx;
        xRotation -= camroty;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if (isShaking)
        {
            float randomX = Random.Range(-shakeStrength, shakeStrength);
            float randomY = Random.Range(-shakeStrength, shakeStrength);

            transform.Rotate(randomY, randomX, 0);

            shakeDuration -= Time.deltaTime;

            if (shakeDuration <= 0)
            {
                isShaking = false;
            }
        }
    }

    public void ShakeCamera(float strength, float duration)
    {
        isShaking = true;
        shakeStrength = strength;
        shakeDuration = duration;
    }

    public void ShakeCamera2()
    {
        isShaking = true;
        shakeStrength = 0.5f;
        shakeDuration = 1f;
    }

    public void ShakeCamera()
    {
        isShaking = true;
        shakeStrength = 2f;
        shakeDuration = 1f;
    }

    public void SetSens (float camSens)
    {
        sens = camSens;
    }

}
