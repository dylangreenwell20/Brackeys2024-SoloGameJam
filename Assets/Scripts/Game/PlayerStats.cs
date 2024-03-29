using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int attack;
    public int defence;
    public int actionPoints;
    public int depth;
    public int healthPotions;

    public bool isDead; //is player dead
    public bool isBlocking; //is player blocking

    private void Awake()
    {
        maxHealth = 100;
        health = maxHealth;
        attack = 5;
        defence = 5;
        actionPoints = 3;
        depth = 0;
        healthPotions = 1;

        isDead = false;
        isBlocking = false;
    }

    public bool TakeDamage(int damage)
    {
        health -= damage; //take away damage from health

        if(health <= 0 ) //if health less than or equal to 0
        {
            health = 0;
            isDead = true;

            return true;
        }

        return false;
    }

    public void ResetStats()
    {
        maxHealth = 100;
        health = maxHealth;
        attack = 5;
        defence = 5;
        actionPoints = 3;
        depth = 0;
        healthPotions = 1;

        isDead = false;
        isBlocking = false;
    }
}
