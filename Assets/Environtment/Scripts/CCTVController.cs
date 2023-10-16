using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CCTVController : MonoBehaviour, IInteractable
{
    public string name;
    public String interactText;
    [SerializeField] private Transform camHolder;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform defaultCamHolder;
    [SerializeField] private bool isLooking;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isLooking)
        {
            mainCamera.transform.position = defaultCamHolder.position;
            cameraController.enabled = true;
            isLooking = false;
        }
        if (isLooking)
        {
            ChangeCamera();
        }
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact()
    {
        //SceneManager.LoadScene("Prototype-CCTV System");
        ChangeCamera();
        isLooking = true;
    }

    private void ChangeCamera(){
        mainCamera.transform.rotation = camHolder.transform.rotation;
        cameraController.enabled = false;
        mainCamera.transform.position = camHolder.transform.position;
    }

}
