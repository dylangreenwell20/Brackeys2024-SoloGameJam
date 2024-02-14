using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatButtons : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    [NonSerialized] public GameObject playerSprite;
    [NonSerialized] public GameObject enemySprite;

    public bool isPlayer;
    public bool spritesCollected;

    private void Start()
    {
        isPlayer = true; //player starts always
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
        //int damage, bool isPlayer;

        //check if player or enemy is attacking

        if (isPlayer)
        {
            //damage enemy

            playerSprite.GetComponent<Animator>().SetTrigger("Attack1"); //play attack animation

            isPlayer = false;
        }

        else
        {
            //check if blocking / damage player

            //get enemy damage

            //run damage method in PlayerStats


            playerSprite.GetComponent<Animator>().SetTrigger("Hurt"); //play hurt animation

            isPlayer = true;
        }
    }

    public void Block()
    {
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
