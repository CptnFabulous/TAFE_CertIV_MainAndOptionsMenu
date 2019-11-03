using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public SpawnWave[] waves;
    SpawnWave currentWave;
    int waveIndex; // Number to determine current wave. Starts at zero and goes up, e.g. if this value is 3 it means we are on wave 4.

    bool waveActive = false;

    public Button startWave;

    public Text waveStatus;
    public string prepareForWave;
    public string waveIsActive;

    public GameObject gameManager;

    // Use this for initialization
    void Start()
    {
        NewWave(waveIndex); // Disable all waves
        //currentWave = waves[0]; // Selects first wave

        startWave.onClick.AddListener(StartWave); // Checks for start wave button to be clicked so it can start the wave
    }

    // Update is called once per frame
    void Update()
    {

        /*
        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (activeEnemies.Length == 0)
        {
            print("No enemies active");
        }
        */

        GameObject[] activeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (activeEnemies.Length == 0 && currentWave.allEnemiesSpawned == true && waveActive == true) // Supposed to check that the wave has finished spawning enemies and that they have all reached the endpoint or been killed.
        {
            print("Wave " + currentWave + " ended"); // Prints that the current wave has ended

            if (waveIndex == waves.Length - 1) // If last wave
            {
                gameManager.GetComponent<PauseMenu>().WinScreen();
            }
            else
            {
                waveIndex += 1;
                NewWave(waveIndex);
            }
        }
    }


    void NewWave(int waveNumber)
    {
        foreach (SpawnWave w in waves) // Checks each wave
        {
            w.gameObject.SetActive(false); // Disables all waves
        }
        currentWave = waves[waveNumber]; // Meant to advance wave by one
        waveActive = false; // Declares that there is not a wave active
        // waveStatus.text = prepareForWave + " " + waveNumber + 1; // Changes hud text to reflect no wave being active
        waveStatus.text = prepareForWave + (waveNumber + 1).ToString(); // Changes hud text to reflect no wave being active
    }

    void StartWave() // Starts the wave
    {
        currentWave.gameObject.SetActive(true); // Activates current wave
        waveActive = true; // Activates bool to say that the wave is active
        waveStatus.text = waveIsActive; // Changes HUD text to say the wave is active
    }

    
}
