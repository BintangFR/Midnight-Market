using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camera;
    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private GameObject GameOverScreen;

    public bool isPaused;
    void Awake() {
        instance = this;    
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
    
}
