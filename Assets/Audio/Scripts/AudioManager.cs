using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioGroup ambienceGroup;
    [SerializeField] private AudioGroup bgmGroup;
    [SerializeField] private AudioGroup enemyGroup;
    [SerializeField] private AudioGroup playerGroup;
    [SerializeField] private AudioGroup sfxGroup;

    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource bgmSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayBGM(2);
    }

    public void PlaySFX(int id, Vector3 position)
    {
        if (id < 0 || id >= sfxGroup.audioList.Length)
        {
            Debug.LogWarning(sfxGroup.name + " (index = " + id + ") not found.");
            return;
        }

        AudioGroup.PreparedAudio temp = sfxGroup.audioList[id];

        AudioSource.PlayClipAtPoint(temp.audioClip, position, temp.volume);
    }

    public void PlaySFX(string audioName, Vector3 position)
    {
        AudioGroup.PreparedAudio temp = GetPreparedAudio(sfxGroup, audioName);

        if (temp == null)
        {
            Debug.LogWarning(sfxGroup.name + " (\"" + audioName + "\") not found.");
            return;
        }

        AudioSource.PlayClipAtPoint(temp.audioClip, position, temp.volume);
    }

    public void PlayAmbience(int id)
    {
        PlayLoopingAudio(ambienceSource, ambienceGroup, id);
    }

    public void PlayAmbience(string audioName)
    {
        PlayLoopingAudio(ambienceSource, ambienceGroup, audioName);
    }

    public void PlayBGM(int id)
    {
        PlayLoopingAudio(bgmSource, bgmGroup, id);
    }

    public void PlayBGM(string audioName)
    {
        PlayLoopingAudio(bgmSource, bgmGroup, audioName);
    }

    private void PlayLoopingAudio(AudioSource audioSource, AudioGroup audioGroup, int id)
    {
        if (id < 0 || id >= audioGroup.audioList.Length)
        {
            Debug.LogWarning(audioGroup.name + " (index = " + id + ") not found.");
            return;
        }

        AudioGroup.PreparedAudio temp = audioGroup.audioList[id];

        audioSource.Stop();

        audioSource.clip = temp.audioClip;
        audioSource.volume = temp.volume;
        audioSource.pitch = temp.pitch;

        audioSource.Play();
    }

    private void PlayLoopingAudio(AudioSource audioSource, AudioGroup audioGroup, string audioName)
    {
        AudioGroup.PreparedAudio temp = GetPreparedAudio(audioGroup, audioName);

        if (temp == null)
        {
            Debug.LogWarning(audioGroup.name + " (\"" + audioName + "\") not found.");
            return;
        }

        audioSource.Stop();

        audioSource.clip = temp.audioClip;
        audioSource.volume = temp.volume;
        audioSource.pitch = temp.pitch;

        audioSource.Play();
    }

    private AudioGroup.PreparedAudio GetPreparedAudio(AudioGroup audioGroup, string audioName)
    {
        for (int i = 0; i < audioGroup.audioList.Length; i++)
        {
            if (audioGroup.audioList[i].audioClip.name == audioName)
            {
                return audioGroup.audioList[i];
            }
        }
        return null;
    }
}
