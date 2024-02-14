using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CombatButtons : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    [NonSerialized] public GameObject playerSprite;
    [NonSerialized] public GameObject enemySprite;

    private ChangeUI changeUI;
    private PlayerStats playerStats;
    private EnemyAI enemyAI;

    [SerializeField] private int damage;
    [SerializeField] private int defence;

    public bool isPlayer;
    public bool isPlayerDead;
    public bool isEnemyDead;
    public bool spritesCollected;

    private void Start()
    {
        isPlayer = true; //player starts always
        isPlayerDead = false;
        isEnemyDead = false;
        damage = 0;
        defence = 0;

        changeUI = this.GetComponent<ChangeUI>(); //get change ui script
        playerStats = this.GetComponent<PlayerStats>(); //get player stats script
        enemyAI = this.GetComponent<EnemyAI>(); //get enemy ai script

        playerSprite = GetSprites(true); //get player sprite
        enemySprite = GetSprites(false); //get enemy sprite

        if (playerSprite == null | enemySprite == null) //if either player sprite or enemy sprite have not been collected
        {
            Debug.Log("Either player or enemy sprite have not been collected..."); //debug message
            return; //return method
        }
    }

    public void Attack()
    {
        if(isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        //check if player or enemy is attacking

        if (isPlayer) //if it is players turn
        {
            playerStats.isBlocking = false; //player not blocking anymore

            damage = playerStats.attack; //get player attack
            defence = enemyAI.defence; //get enemy defence

            //def is taken off of damage - e.g. 15 damage with 10 defence results in the attack dealing 5 damage

            int calculatedDamage = damage - defence; //calculate damage with defence reduction

            if(calculatedDamage < 0) //if damage is less than 0
            {
                calculatedDamage = 0; //set damage to 0 to avoid negative damage (which heals instead of damaging)
            }

            isEnemyDead = enemyAI.TakeDamage(calculatedDamage); //run take damage method in enemy ai script using calculated damage

            changeUI.UpdateStatsBattleUI(); //update battle ui

            playerSprite.GetComponent<Animator>().SetTrigger("Attack1"); //play attack animation

            if (isEnemyDead)
            {
                //play smoke animation?

                enemySprite.SetActive(false); //hide enemy
            }

            isPlayer = false; //no longer players turn
        }

        else //else if it is enemys turn
        {
            damage = enemyAI.damage; //get enemy damage
            defence = playerStats.defence; //get player defence

            //Debug.Log(damage);
            //Debug.Log(defence);

            //def is taken off of damage - e.g. 15 damage with 10 defence results in the attack dealing 5 damage

            if (playerStats.isBlocking) //if blocking
            {
                //Debug.Log(defence);
                defence *= 2; //defence multiplied by 2
                //Debug.Log(defence);
            }

            int calculatedDamage = damage - defence; //apply defence reduction to damage

            if(calculatedDamage < 0) //if damage is less than 0
            {
                calculatedDamage = 0; //set damage to 0 (to avoid negative damage healing the player)
            }

            isPlayerDead = playerStats.TakeDamage(calculatedDamage); //run take damage method in player stats using calculated damage

            changeUI.UpdateStatsBattleUI(); //update battle UI

            if (isPlayerDead) //if player is dead
            {
                //death/game over code

                playerSprite.GetComponent<Animator>().SetTrigger("Death"); //play death animation
            }
            else //else if player is alive still
            {
                playerSprite.GetComponent<Animator>().SetTrigger("Hurt"); //play hurt animation
            }

            isPlayer = true; //now it is players turn
        }
    }

    public void Block()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        playerStats.isBlocking = true; //player is blocking

        playerSprite.GetComponent<Animator>().SetTrigger("Block"); //play block animation
        playerSprite.GetComponent<Animator>().SetBool("IdleBlock", true); //play idle block animation
    }

    public GameObject GetSprites(bool isPlayerSprite)
    {
        if(isPlayerSprite) //if method needs to get player sprite
        {
            GameObject playerSprite = player.transform.GetChild(0).gameObject; //get player sprite

            if (playerSprite != null) //if player sprite was found
            {
                Debug.Log("Player sprite collected..."); //debug message
                return playerSprite; //return player sprite
            }
            else
            {
                Debug.Log("No player sprite found..."); //debug message
                return null; //return null
            }
        }
        else //else if method needs to get enemy sprite
        {
            GameObject enemySprite = enemy.transform.GetChild(0).gameObject; //get current enemy sprite

            if (enemySprite != null) //if enemy sprite was found
            {
                Debug.Log("Enemy sprite collected..."); //debug message
                return enemySprite; //return enemy sprite
            }
            else
            {
                Debug.Log("No enemy sprite found..."); //debug message
                return null; //return null
            }
        }


    }
}
