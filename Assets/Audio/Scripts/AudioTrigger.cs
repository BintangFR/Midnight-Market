using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private int audioID;
    [SerializeField] private Transform soundSource;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioManager != null)
            {
                Vector3 soundPosition = soundSource != null ? soundSource.position : transform.position;
                audioManager.PlaySFX(audioID, soundPosition);
            }
        }
    }
}
