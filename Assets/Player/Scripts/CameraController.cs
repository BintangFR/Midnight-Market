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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Camera.main.nearClipPlane = minClippingDistance;

        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
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
    }

    public void SetSens (float camSens)
    {
        sens = camSens;
    }
}
