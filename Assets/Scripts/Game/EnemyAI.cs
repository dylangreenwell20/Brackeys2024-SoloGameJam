using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy enemy; //get enemy scriptable object

    public Transform slimePosition; //position of slime
    public Transform skeletonPosition; //position of skeleton

    public int health; //health of enemy
    public int defence; //defence of enemy
    public int damage; //damage of enemy

    private void Start()
    {
        enemy = null;
        health = 0;
        defence = 0;
        damage = 0;
    }

    //active enemy - have all sprite positions contain each enemy - just only show the current enemy that is loaded
    //rooms with multiple enemies will just show the enemy at the top of the stack/front of the array
}
