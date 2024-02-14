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

    public bool isDead; //is player dead

    private void Start()
    {
        maxHealth = 100;
        health = maxHealth;
        attack = 10;
        defence = 10;
        actionPoints = 5;
        depth = 0;

        isDead = false;
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
}
