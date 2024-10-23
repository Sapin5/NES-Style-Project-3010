using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string[] gameScenes;
    public static bool gamePaused = false;
    private static bool mainMenu = true;
    [SerializeField] private GameObject pauseMenuUI;

    public void PlayGame(){
        SceneManager.LoadScene(gameScenes[1]);
        Time.timeScale = 1;
        mainMenu = false;
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene(gameScenes[0]);
        mainMenu = true;
    }

    public void QuitGame(){
        Application.Quit();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && !mainMenu){
            if(gamePaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    private void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void Controls(){
        SceneManager.LoadScene(gameScenes[2]);
    }
}
