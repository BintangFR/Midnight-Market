using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAudio : MonoBehaviour
{
    private PlayerController playerController;

    public AudioSource footstepsSound, sprintSound, jumpSound;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && playerController.isGrounded && playerController.canMove)
        {
            if (playerController.isRunning == true)
            {
                footstepsSound.enabled = false;
                sprintSound.enabled = true;              
            }
            else
            {
                footstepsSound.enabled = true;
                sprintSound.enabled = false;                            
            }
        }
        else
        {
            footstepsSound.enabled = false;
            sprintSound.enabled = false;
        }

        if (playerController.isGrounded == false){
            jumpSound.enabled = true;
        }
        else
        {
            jumpSound.enabled = false;
        }
    }
}
