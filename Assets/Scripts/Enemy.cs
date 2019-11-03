using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int healthMax;
    public int healthCurrent;
    int healthPrev;

    public float movementSpeed;
    public int damageToPoint;

    public int currencyDropped;
    

    public Transform target;

    GameObject player;

    NavMeshAgent na;


    

    // Use this for initialization
    void Start()
    {
        na = GetComponent<NavMeshAgent>(); // Obtain navmesh agent
        na.speed = movementSpeed; // Changes agent's movement speed to equal speed specified in the inspector

        player = GameObject.FindGameObjectWithTag("MainCamera");
        
        if (target == null) // If a target is not assigned
        {
            AssignNearestTarget(); // Assign a target
        }

        healthPrev = healthCurrent; // healthPrev is updated to match healthCurrent for checking next frame
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (na.destination == null)
        {
            AssignNearestTarget();
        }
        */

        na.destination = target.transform.position; // Move towards target

        
        if (healthPrev != healthCurrent) // Checks if health has changed
        {
            healthCurrent = Mathf.Clamp(healthCurrent, 0, healthMax); // Clamps healthCurrent to not exceed healthMax or fall below zero
            if (healthCurrent <= 0) // If the player has zero or less health points
            {
                Die(); // Enemy dies
            }
        }
        healthPrev = healthCurrent; // healthPrev is updated to match healthCurrent for checking next frame
        
    }

    void AssignNearestTarget() // Assigns the target closest to the enemy
    {
        GameObject[] targetList = GameObject.FindGameObjectsWithTag("EnemyTarget"); // Finds all target waypoints
        GameObject closestTarget = targetList[0]; // Sets closestTarget variable to first target in array for checking
        
        foreach (GameObject ct in targetList) // For every target found
        {
            if (Vector3.Distance(transform.position, ct.transform.position) < Vector3.Distance(transform.position, closestTarget.transform.position)) // Determines if the target is closer than closestTarget
            {
                closestTarget = ct; // if so, the new target becomes closestTarget
            }
        }

        target = closestTarget.transform; // the closest target (represented by the 'closestTarget' variable) is assigned as the enemy's target
    }
    
    public void Damage(int damageAmount) // Used for taking damage. This function can be declared from other scripts to deal damage to the enemy, with damageAmount referring to the damage dealt
    {
        healthCurrent -= damageAmount; // Subtracts an amount from healthCurrent equal to damageAmount
        // Add other effects
    }

    void Die() // Enemy dies
    {
        player.GetComponent<PlayerResources>().currency += currencyDropped;
        Destroy(gameObject); // Enemy vanishes from the scene
        // Add or substitute alternate effects for enemy dying, as the enemy simply vanishing looks weird
    }
    
    
    
}
