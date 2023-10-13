using System.Collections;
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

    private AudioSource enemySource;
    private AudioSource enemyVocalSource;
    private float enemyVocalDuration = 0f;
    private AIController.EnemyState enemyState;

    private float audioCooldown = 0;

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

        //PlayBGM(2);
    }

    private void Start()
    {
        GameObject enemyHead = AIController.Instance.GetAIVision().gameObject;

        enemySource = AIController.Instance.GetComponent<AudioSource>();
        enemyVocalSource = enemyHead.GetComponent<AudioSource>();
    }

    public void ChangeEnemyState(AIController.EnemyState enemyState)
    {
        this.enemyState = enemyState;
    }

    private void Update()
    {
        audioCooldown += Time.deltaTime;

        if (audioCooldown > enemyVocalDuration)
        {
            audioCooldown = 0f;
            enemyVocalDuration = 0f;

            if (enemyState == AIController.EnemyState.chasing)
            {
                string[] chasingAudios =
                {
                    "Laughing 1",
                    "Laughing 2",
                    "Laughing 3"
                };

                AudioGroup.PreparedAudio selectedAudio = GetPreparedAudio(enemyGroup, chasingAudios);

                enemyVocalSource.clip = selectedAudio.audioClip;

                enemyVocalDuration = selectedAudio.audioClip.length;

                enemyVocalSource.Play();
            }
            else if (enemyState == AIController.EnemyState.seeking)
            {
                string[] seekingAudios =
                {
                    "Vocal Humans Are Inferior",
                    "Vocal I Love Killing Humans",
                    "Vocal I Will Have My Revenge",
                    "Vocal It's Too Late To Run",
                    "Vocal They Say Robots Have No Hearts",
                    "Jingle",
                    "Vocal You Will Die"
                };

                AudioGroup.PreparedAudio selectedAudio = GetPreparedAudio(enemyGroup, seekingAudios);

                if (selectedAudio == null)
                {
                    return;
                }

                enemyVocalSource.clip = selectedAudio.audioClip;

                enemyVocalDuration = selectedAudio.audioClip.length;

                enemyVocalSource.Play();
            }
        }
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

    public void PlayEnemy(int id)
    {
        PlayLoopingAudio(enemySource, enemyGroup, id);
    }

    public void PlayEnemy(string audioName)
    {
        PlayLoopingAudio(enemySource, enemyGroup, audioName);
    }

    public void StopAmbience()
    {
        ambienceSource.Stop();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void StopEnemy()
    {
        enemySource.Stop();
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
        Debug.Log(audioName);
        return null;
    }

    private AudioGroup.PreparedAudio GetPreparedAudio(AudioGroup audioGroup, string[] audioNames)
    {
        int randomIndex = Random.Range(0, audioNames.Length);

        return GetPreparedAudio(audioGroup, audioNames[randomIndex]);
    }
}
