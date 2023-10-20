using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainAmbienceTrigger : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int audioID;
    [SerializeField] private AudioClip rainElectricClip;
    [SerializeField] private AudioClip defaultAudioClip;
    private void OnTriggerEnter(Collider other) {
        audioSource.clip = rainElectricClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    
    private void OnTriggerExit(Collider other) {
        audioSource.clip = defaultAudioClip;
        audioSource.loop = true;
        audioSource.Play();
        
        
    
    }
}
