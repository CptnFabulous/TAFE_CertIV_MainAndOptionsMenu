using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    public GameObject endpoint; // The endpoint itself

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider c) // If something breaches the trigger
    {
        print("Collision detected"); // Console line to ensure it has collided
        
        if (c.CompareTag("Enemy") == true) // Check if it's an enemy
        {
            print(c.name + " has reached the endpoint!"); // Console line to ensure the enemy has breached the trigger
            // GameObject endpoint = c.GetComponent<EndTrigger>().endpoint; // THIS LINE IS USELESS
            endpoint.GetComponent<Endpoint>().Damage(c.GetComponent<Enemy>().damageToPoint); // Have enemy deal damage to endpoint
            Destroy(c.gameObject); // Destroy enemy. This needs to be replaced with an actual function on the enemy, for however we want to remove them from the scene.
        }
        
    }
}
