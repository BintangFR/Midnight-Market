using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform cameraPosition;
    public Transform orientation;
    public float sensx;
    public float sensy;
    private float yRotation;
    private float xRotation;
    public float minClippingDistance = 0.1f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera.main.nearClipPlane = minClippingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate camera based of sensitivity
        Camera.main.transform.position = cameraPosition.transform.position;
        float camrotx = Input.GetAxis("Mouse X") * sensx * Time.deltaTime;
        float camroty = Input.GetAxis("Mouse Y") * sensy * Time.deltaTime;

        yRotation += camrotx;
        xRotation -= camroty;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
