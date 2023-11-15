using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskboardController : MonoBehaviour,IInteractable
{
    [SerializeField] private GameObject taskboardImage;
    public UnityEvent unityEvent;
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    private bool isLooking;
    private bool hasInteract;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.isLooking = isLooking;
        if (Input.GetKeyDown(KeyCode.Escape) && isLooking)
        {
            NotLookTaskboard(); 
        }

    }

    public string GetInteractText()
    {
        return "Look Taskboard";
    }

    public void Interact()
    {
        if (!hasInteract)
        {
            LookTaskboard();
            unityEvent.Invoke();
        }
        else{
            LookTaskboard();
        }
    }

    private void LookTaskboard(){
        player.enabled = false;
        camera.enabled = false;
        isLooking = true;
        taskboardImage.SetActive(true);
        hasInteract = true;

    }
    private void NotLookTaskboard(){
        player.enabled = true;
        camera.enabled = true;
        isLooking = false;
        taskboardImage.SetActive(false);
    }
}
