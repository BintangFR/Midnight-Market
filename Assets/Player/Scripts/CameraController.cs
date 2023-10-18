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
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
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
