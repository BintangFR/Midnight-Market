using System;
using UnityEngine;

[CreateAssetMenu]
public class AudioGroup : ScriptableObject
{
    [Serializable]
    public class PreparedAudio
    {
        public AudioClip audioClip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(-3f, 3f)] public float pitch = 1f;
    }

    public PreparedAudio[] audioList;
}
