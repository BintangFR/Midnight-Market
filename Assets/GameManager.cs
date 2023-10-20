using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject FinishedScreen;

    public bool isPaused;
    void Awake() {

    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    void Awake() {
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
    }

    void Start()
    {
        // AudioManager.Instance.PlayBGM("Music Box");
        pausedMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausedGame();
        }
    }

    public void FinishGame(){
        player.enabled = false;
        camera.enabled = false;
        FinishedScreen.SetActive(true);
        Debug.Log("game complete");
    }
    public void GameOver(){

        player.enabled = false;
        camera.enabled = false;
        GameOverScreen.SetActive(true);    
    }
    public void RestartGame(){
        SceneManager.LoadScene("MainGame(Prototype)");
    }
    public void PausedGame(){
        Time.timeScale = 0f;
        pausedMenu.SetActive(true);
        isPaused = true;
    }
    public void ResumeGame(){
        Time.timeScale = 1f;
        pausedMenu.SetActive(false);
        isPaused = false;
    }
    public void GoToMainMenu(){
        Time.timeScale = 1f;
        pausedMenu.SetActive(false);
        SceneManager.LoadScene("Prototype-MainMenu");
    }
    
        player.enabled = false;
        camera.enabled = false;
        
    }
}
