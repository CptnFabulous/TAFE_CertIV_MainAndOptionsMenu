using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public int damage = 10;
    public float attackRate = 1f;
    protected Enemy currentEnemy;
    private float attackTimer = 0f;
    public float attackRange = 2f;
    //aims at given enemy every frame 
    protected virtual void Aim(Enemy e) { }
    //attacks a given enemy 
    public virtual void Attack(Enemy e) { }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    // protected - canncon has acess other tower classes 
    //virtual - able to change what this function dose in derive
    void DetectEnemies()
    {
        //reset enemy
        currentEnemy = null;
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var hit in hits)
        {
            // If the thing we hit is an enemy
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy)
            {
                // Set current enemy to that one
                currentEnemy = enemy;
            }
        }
    }



    protected virtual void Update()
    {
        DetectEnemies();
        //count timmer 
        attackTimer += Time.deltaTime;

        if (currentEnemy)
        {
           
            // Aim at the enemy
            Aim(currentEnemy);
            // Is attack timer ready?
            if (attackTimer >= attackRate)
            {
                // Attack the enemy!
                Attack(currentEnemy);
                // Reset timer
                attackTimer = 0f;
            }
        }
    }
}
