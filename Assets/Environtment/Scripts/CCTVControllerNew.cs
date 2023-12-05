using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CCTVControllerNew : MonoBehaviour, IInteractable
{
    public string name;
    public string interactText;
    public GameObject cctvUI;
    public TextMeshProUGUI cameraLabel;
    public Camera[] cameras;
    private int currentCameraIndex = 0;

    private bool isCCTVActive = false;

    public PlayerController playerController;
    public GameObject mainCamera;
    public GameObject enemy;

    private bool hasInteracted = false;

    [SerializeField] private NavMeshAgent shadowMan;
    [SerializeField] private Transform shadowManTarget;
    [SerializeField] private float shadowManDissappearence;
    [SerializeField] private bool canExitCCTV;
    public UnityEvent unityEvent;

    void Start()
    {
        cctvUI.SetActive(false);
        
    }

    void Update()
    {
        if (isCCTVActive && Input.GetKeyDown(KeyCode.Backspace))
        {
            ExitCCTV();
        }
        else if (isCCTVActive && Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchToNextCamera();
        }
        else if (isCCTVActive && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchToPreviousCamera();
        }
    }

    public string GetInteractText()
    {
        return interactText;
    }

    private void SetCameraLabel(int index)
    {
        if (cameraLabel != null)
        {
            cameraLabel.text = "Camera " + (index + 1).ToString("D2");
        }
    }

    private void OpenCCTV()
    {
        mainCamera.SetActive(false);
        isCCTVActive = true;
        cctvUI.SetActive(true);
        playerController.canMove = false;
        gameObject.layer = 0;

        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        cameras[0].gameObject.SetActive(true);
    }

    private void ExitCCTV()
    {
        if (canExitCCTV)
        {
            
            mainCamera.SetActive(true);
            cctvUI.SetActive(false);
            isCCTVActive = false;
            playerController.canMove = true;
            gameObject.layer = 7;

            //deactivate all cctv camera
            foreach (Camera camera in cameras)
            {
                camera.gameObject.SetActive(false);
            }
        }

        //deactivate enemy
        //enemy.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (!isCCTVActive)
        {
            //Trigger cutscene pertama kali
            if (!hasInteracted)
            {
                Debug.Log("Cutscene Muncul");
                OpenCCTV();
                StartCoroutine(Cutscene());
                hasInteracted = true;
                
            }

            OpenCCTV();
            SetCameraLabel(0);
        }
    }
    
    private IEnumerator Cutscene(){
        enemy.gameObject.SetActive(true);
        LightController.Instance.SetLighting(true);
        yield return new WaitForEndOfFrame();
        shadowMan.SetDestination(shadowManTarget.position);
        yield return new WaitForSeconds(shadowManDissappearence);
        canExitCCTV = true;
        yield return new WaitForEndOfFrame();
        ExitCCTV();
        unityEvent.Invoke();
        yield return new WaitForEndOfFrame();
        LightController.Instance.SetLighting(false);
        enemy.gameObject.SetActive(false);
        
    }

    private void SwitchToNextCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);
        

        // Move to the next camera 
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable the next camera
        cameras[currentCameraIndex].gameObject.SetActive(true);
        

        // Update the camera label
        SetCameraLabel(currentCameraIndex);
    }

    private void SwitchToPreviousCamera()
    {
      
        cameras[currentCameraIndex].gameObject.SetActive(false);
        
       
        currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;

        
        cameras[currentCameraIndex].gameObject.SetActive(true);
        
        
        SetCameraLabel(currentCameraIndex);
    }

}
