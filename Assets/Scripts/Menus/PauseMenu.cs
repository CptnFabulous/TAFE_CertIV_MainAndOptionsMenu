using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Disable on Pause")]
    public Canvas headsUpDisplay;
    public TowerPlacement playerInput;

    [Header("Enable on Pause")]
    public Canvas pauseMenu;

    [Header("Win and Fail States")]
    public Canvas winScreen;
    public Canvas failScreen;

    bool isPaused;
    bool gameEnded;

    // Use this for initialization
    void Start()
    {
        Unpause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && gameEnded == false)
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        headsUpDisplay.gameObject.SetActive(false);
        playerInput.enabled = false;
        pauseMenu.gameObject.SetActive(true);
        isPaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        headsUpDisplay.gameObject.SetActive(true);
        playerInput.enabled = true;
        pauseMenu.gameObject.SetActive(false);
        isPaused = false;

        winScreen.gameObject.SetActive(false);
        failScreen.gameObject.SetActive(false);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void WinScreen() // Disables pause and 
    {
        headsUpDisplay.gameObject.SetActive(false);
        playerInput.enabled = false;
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        gameEnded = true;
        winScreen.gameObject.SetActive(true);
        failScreen.gameObject.SetActive(false);
    }

    public void FailScreen()
    {
        headsUpDisplay.gameObject.SetActive(false);
        playerInput.enabled = false;
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        gameEnded = true;
        failScreen.gameObject.SetActive(true);
        winScreen.gameObject.SetActive(false);
    }
}
