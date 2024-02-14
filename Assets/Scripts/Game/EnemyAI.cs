using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy enemy; //get enemy scriptable object

    public string enemyName; //name of enemy

    public int maxHealth; //max health of enemy
    public int health; //health of enemy
    public int defence; //defence of enemy
    public int damage; //damage of enemy

    public bool isDead; //is enemy dead

    private void Start()
    {
        enemyName = enemy.name; //get enemy name
        health = enemy.health; //get enemy health
        maxHealth = health; //set max health to health value
        defence = enemy.defence; //get enemy defence
        damage = enemy.damage; //get enemy damage
    }

    public bool TakeDamage(int damage)
    {
        health -= damage; //take away damage from health

        if (health <= 0) //if health less than or equal to 0
        {
            health = 0;
            isDead = true;

            return true;
        }

        return false;
    }

    //active enemy - have all sprite positions contain each enemy - just only show the current enemy that is loaded
    //rooms with multiple enemies will just show the enemy at the top of the stack/front of the array
}
