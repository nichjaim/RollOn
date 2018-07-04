using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject levelCompleteMenuUI;
    public GameObject pauseButtonUI;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        /*
        //temp conditional until official pause button properly implemented
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        */
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseButtonUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseButtonUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        //Debug.Log("Loading Menu . . .");
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void levelComplete()
    {
        pauseMenuUI.SetActive(false);
        pauseButtonUI.SetActive(false);
        levelCompleteMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void loadNextLevel()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        //TODO: Write way to find next level
        //SceneManager.LoadScene(nextLevelNameStringVariable);

        //placeholder code: return to main menu and describe proper function in console
        Debug.Log("Loading Next Level . . .");
        SceneManager.LoadScene("MainMenu");
    }
}
