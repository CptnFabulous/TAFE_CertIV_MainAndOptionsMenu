using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public GameObject[] menus;
    public string gameSceneName;

    //public Scene firstLevel;

    /*
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    */

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ChangeMenu(GameObject menu)
    {
        foreach (GameObject m in menus)
        {
            m.SetActive(false);
            menu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
