using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private bool playAudio;
    [SerializeField] private string audioName;
    [SerializeField] private Transform soundSource;
    [SerializeField] private TriggerType triggerType;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Ambience)
            {
                AudioManager.Instance.PlayAmbience(audioName);
                Debug.Log("true");
            }
            else if (triggerType == TriggerType.SFX)
            {
                Vector3 soundPosition = soundSource != null ? soundSource.position : transform.position;
                AudioManager.Instance.PlaySFX(audioName, soundPosition);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //AudioManager.Instance.StopAmbience();
            AudioManager.Instance.PlayAmbience("Rain-Ambience");
        }
    }
}

public enum TriggerType
{
    Ambience,
    BGM,
    SFX
}
