using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private int audioID;
    [SerializeField] private bool playAudio;
    [SerializeField] private AudioSource audioSource;
    private void OnTriggerEnter(Collider other){
        //playAudio = !playAudio;
        if (other.gameObject == GameObject.Find("Player")){
            AudioManager.Instance.PlayAmbience(audioID);
            
        }
        
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject == GameObject.Find("Player"))
        {
            audioSource.Stop();
        }
    }

    
}
