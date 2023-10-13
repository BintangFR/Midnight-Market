using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishGame(){
        Debug.Log("game complete");
    }
}
