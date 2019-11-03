using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Endpoint : MonoBehaviour
{
    public int healthMax;
    public int healthCurrent;
    int healthPrev;

    public Text counter;

    public GameObject gameManager;

    // Use this for initialization
    void Start()
    {
        counter.text = "HP:  " + healthCurrent + "/" + healthMax;
        healthPrev = healthCurrent; // Updates healthPrev so it can be used to check healthCurrent next frame
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPrev != healthCurrent) // If health changes
        {
            healthCurrent = Mathf.Clamp(healthCurrent, 0, healthMax); // Clamps health between maximum and zero
            counter.text = "HP:  " + healthCurrent + "/" + healthMax;
            if (healthCurrent <= 0) // If health is zero or less
            {
                PointDestroyed(); // Initiate failure state
            }
        }
        healthPrev = healthCurrent; // Updates healthPrev so it can be used to check healthCurrent next frame
    }

    public void Damage(int damageAmount)
    {
        healthCurrent -= damageAmount;
        print("The " + name + " took " + damageAmount + " damage! " + healthCurrent + " out of " + healthMax + " remaining!");
        //counter.text = "HP:  " + healthCurrent + "/" + healthMax;
    }

    public void PointDestroyed()
    {
        //Destroy(gameObject);
        gameManager.GetComponent<PauseMenu>().FailScreen();
        // Initiate appropriate failure code e.g. failure screen, sad trombone music etc.
    }
}
