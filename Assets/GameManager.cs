using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject FinishedScreen;
    [SerializeField] private GameObject ObjectiveScreen;
    [SerializeField] private GameObject OptionsScreen;
    [SerializeField] private GameObject controlScreen;
    
    private AudioSource[] audios;
    private bool[] isAudioPlaying;
    public UIState uiState;

    public enum UIState{
        Objective = 0,
        Options = 1,
        Control = 2

    }
    public bool isPaused;

    public bool isLooking; //Look at something else
    
    public static GameManager Instance { get; private set; }

    private UIState currentState;
    void Awake() {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    void Start()
    {
        currentState = UIState.Objective;
        // AudioManager.Instance.PlayBGM("Music Box");
        pausedMenu?.SetActive(false);
        GameOverScreen?.SetActive(false);
        FinishedScreen?.SetActive(false);
        audios = FindObjectsOfType<AudioSource>();
        isAudioPlaying = new bool[audios.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isLooking)
        {
            PausedGame();
        }
        if (!isPaused)
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        switch (currentState)
        {
            case UIState.Objective:
                ObjectiveScreen?.SetActive(true);
                OptionsScreen?.SetActive(false);
                controlScreen?.SetActive(false);
                break;
            case UIState.Options:
                ObjectiveScreen?.SetActive(false);
                OptionsScreen?.SetActive(true);
                controlScreen?.SetActive(false);
                break;
            case UIState.Control:
                ObjectiveScreen?.SetActive(false);
                OptionsScreen?.SetActive(false);
                controlScreen?.SetActive(true);                
                break;


        }
    }

    public void FinishGame(){
        player.enabled = false;
        camera.enabled = false;
        FinishedScreen?.SetActive(true);
        Debug.Log("game complete");
        isPaused = true;

    }
    public void GameOver(){

        player.enabled = false;
        camera.enabled = false;
        GameOverScreen?.SetActive(true);
        isPaused = true;

    }
    public void RestartGame(){
        SceneManager.LoadScene(1);
    }

    public void PausedGame(){
        player.enabled = false;
        camera.enabled = false;
        Time.timeScale = 0f;
        currentState = UIState.Objective;
        pausedMenu?.SetActive(true);
        isPaused = true;

        for (int i = 0; i < audios.Length; i++)
        {
            if (audios[i].isPlaying)
            {
                isAudioPlaying[i] = true;
                audios[i].Pause();
            }
        }
    }

    public void ResumeGame(){
        player.enabled = true;
        camera.enabled = true;
        Time.timeScale = 1f;
        pausedMenu?.SetActive(false);
        isPaused = false;

        for (int i = 0; i < audios.Length; i++)
        {
            if (isAudioPlaying[i] && audios[i].isActiveAndEnabled)
            {
                audios[i].Play();
            }
        }
    }
    public void GoToMainMenu(){
        Time.timeScale = 1f;
        pausedMenu?.SetActive(false);
        SceneManager.LoadScene(0);
        
    }

    public void ChangeState(int newState){
        currentState = (UIState)newState;
    }
    public void OpenObjective(){
        ObjectiveScreen?.SetActive(true);

    }

    public void BreakElectricity()
    {
        StartFlickerCycle(10f, 10f);
    }

    public void StartFlickerCycle(float firstCooldown, float secondCooldown)
    {
        StopAllCoroutines();
        StartCoroutine(FlickerCycle(firstCooldown, secondCooldown));
    }

    private IEnumerator FlickerCycle(float firstCooldown, float secondCooldown)
    {
        yield return new WaitForSeconds(firstCooldown);

        int count = Random.Range(2, 5) * 2;
        LightController.Instance.FlickerLight(count);

        yield return new WaitForSeconds(secondCooldown);

        count = Random.Range(2, 5) * 2;
        LightController.Instance.FlickerLight(count);

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 20f));

            count = Random.Range(2, 5) * 2;
            LightController.Instance.FlickerLight(count);
        }
    }
}

