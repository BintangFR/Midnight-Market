using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVSlider : MonoBehaviour
{
    private Camera cam;
    private float defaultFOV;
    [SerializeField] private PlayerController player;


    void Start()
    {

        cam = GetComponent<Camera>();
        cam = Camera.main;
        defaultFOV = cam.fieldOfView;    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.isWalking)
        {
            cam.fieldOfView = defaultFOV - 20;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !player.isWalking)
        {
            cam.fieldOfView = defaultFOV + 20;  
        }

        defaultFOV = cam.fieldOfView;
    }

    public void SetFOV(float fov)
    {
        cam.fieldOfView = fov;
    }
}
