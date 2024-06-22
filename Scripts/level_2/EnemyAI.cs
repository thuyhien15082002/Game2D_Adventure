using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    //public AIPath aIPath;
    public int maxHealth = 100;
    
    int currentHealth;

    // Update is called once per frame
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject);

    }
  
    
}
