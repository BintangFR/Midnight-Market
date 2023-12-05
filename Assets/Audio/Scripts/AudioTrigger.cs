using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private bool playAudio;
    [SerializeField] private string audioName;
    [SerializeField] private Transform soundSource;
    [SerializeField] private TriggerType triggerType;
    private static bool isOnBridge = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Bridge)
            {
                isOnBridge = true;
            }
            if (triggerType == TriggerType.Ambience && !isOnBridge)
            {
                AudioManager.Instance.StopAmbience();
                AudioManager.Instance.PlayAmbience(audioName);
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
            if (triggerType == TriggerType.Bridge)
            {
                isOnBridge = false;
            }
            else if (!isOnBridge)
            {
                AudioManager.Instance.StopAmbience();
                AudioManager.Instance.PlayAmbience("Room Ambience");
            }
        }
    }
}

public enum TriggerType
{
    Ambience,
    BGM,
    SFX,
    Bridge
}
