using System.Collections;
using System.Collections.Generic;
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
    public bool isPaused;
    
    public static GameManager Instance { get; private set; }
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
        player.enabled = false;
        camera.enabled = false;
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
}

