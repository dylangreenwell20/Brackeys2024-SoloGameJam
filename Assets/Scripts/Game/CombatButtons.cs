using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatButtons : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    [NonSerialized] public GameObject playerSprite;
    [NonSerialized] public GameObject enemySprite;

    [SerializeField] private GameObject battleOptions;
    [SerializeField] private GameObject attackOptions;
    [SerializeField] private GameObject defendOptions;
    [SerializeField] private GameObject healOptions;

    [SerializeField] private Button slashButton;
    [SerializeField] private Button smashButton;
    [SerializeField] private Button blockButton;
    [SerializeField] private Button parryButton;
    [SerializeField] private Button healPotButton;
    [SerializeField] private Button healSkillButton;

    private ChangeUI changeUI;
    private PlayerStats playerStats;
    private EnemyAI enemyAI;
    private RoomType roomType;
    private DeathUI deathUI;

    [SerializeField] private string enemyName;

    [SerializeField] private int damage;
    [SerializeField] private int defence;
    [SerializeField] private int criticalChance;

    [SerializeField] private float damageMultiplier;

    public bool isPlayer;
    public bool isPlayerDead;
    public bool isEnemyDead;
    public bool spritesCollected;
    public bool isParrying;

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
        roomType = this.GetComponent<RoomType>(); //get room type script
        deathUI = this.GetComponent<DeathUI>();

        enemyName = enemyAI.enemyName; //get enemy name

        playerSprite = GetSprites(true); //get player sprite
        enemySprite = GetSprites(false); //get enemy sprite

        if (playerSprite == null | enemySprite == null) //if either player sprite or enemy sprite have not been collected
        {
            Debug.Log("Either player or enemy sprite have not been collected..."); //debug message
            return; //return method
        }
    }

    public void Attack(bool abilityUsed)
    {
        isPlayerDead = playerStats.isDead;
        isEnemyDead = enemyAI.isDead;

        if(isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        slashButton.interactable = false;
        smashButton.interactable = false;
        blockButton.interactable = false;
        parryButton.interactable = false;
        healPotButton.interactable = false;
        healSkillButton.interactable = false;

        //check if player or enemy is attacking

        if (isPlayer) //if it is players turn
        {
            playerStats.isBlocking = false; //player not blocking anymore

            damage = playerStats.attack; //get player attack
            defence = enemyAI.defence; //get enemy defence

            //random number between 0.6 and 1.4 for random damage multiplier

            damageMultiplier = UnityEngine.Random.Range(0.6f, 1.4f);

            float tempdamage = (float)damage * damageMultiplier;

            damage = Mathf.RoundToInt(tempdamage); //round float to int and save as damage

            //if ability was used

            if (abilityUsed)
            {
                damage = damage * 2;
            }

            //10% chance to critical hit

            criticalChance = UnityEngine.Random.Range(1, 10); //generate random number between 1 and 10

            if (criticalChance == 1) //if number equal to 1 (10% chance)
            {
                damage = damage * 2; //double damage
                Debug.Log("CRITICAL HIT FOR " + damage + " DAMAGE!!!");
            }

            //def is taken off of damage - e.g. 15 damage with 10 defence results in the attack dealing 5 damage

            int calculatedDamage = damage - defence; //calculate damage with defence reduction

            if(calculatedDamage < 0) //if damage is less than 0
            {
                calculatedDamage = 0; //set damage to 0 to avoid negative damage (which heals instead of damaging)
            }

            isEnemyDead = enemyAI.TakeDamage(calculatedDamage); //run take damage method in enemy ai script using calculated damage

            changeUI.UpdateStatsBattleUI(); //update battle ui

            if (abilityUsed)
            {
                playerSprite.GetComponent<Animator>().SetTrigger("Attack2");
            }
            else
            {
                playerSprite.GetComponent<Animator>().SetTrigger("Attack1"); //play attack animation
            }

            if (isEnemyDead)
            {
                isEnemyDead = false;

                enemy.transform.Find(enemyAI.enemyName).gameObject.SetActive(false); //hide enemy

                StartCoroutine(EnemyDeathTime());

                playerStats.depth += 1; //increase depth

                roomType.GenerateRooms(); //generate new rooms

                enemyAI.ResetEnemy(); //reset enemy

                ResetCombatUI();

                changeUI.ToggleUI(0); //change to door scene

                isPlayer = true;
                playerStats.isBlocking = false;

                slashButton.interactable = true;
                smashButton.interactable = true;
                blockButton.interactable = true;
                parryButton.interactable = true;
                healPotButton.interactable = true;
                healSkillButton.interactable = true;

                return;
            }

            if (isParrying)
            {
                slashButton.interactable = true;
                smashButton.interactable = true;
                blockButton.interactable = true;
                parryButton.interactable = true;
                healPotButton.interactable = true;
                healSkillButton.interactable = true;

                playerStats.isBlocking = false;
                isParrying = false;
            }

            playerStats.isBlocking = false;
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
                defence *= 2; //defence multiplied by 2
            }

            //random number between 0.6 and 1.4 for random damage multiplier

            damageMultiplier = UnityEngine.Random.Range(0.6f, 1.4f);

            float tempdamage = (float)damage * damageMultiplier;

            damage = Mathf.RoundToInt(tempdamage); //round float to int and save as damage

            //10% chance to critical hit

            criticalChance = UnityEngine.Random.Range(1, 10); //generate random number between 1 and 10

            if (criticalChance == 1) //if number equal to 1 (10% chance)
            {
                damage = damage * 2; //double damage
                Debug.Log("CRITICAL HIT FOR " + damage + " DAMAGE!!!");
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

                slashButton.interactable = true;
                smashButton.interactable = true;
                blockButton.interactable = true;
                parryButton.interactable = true;
                healPotButton.interactable = true;
                healSkillButton.interactable = true;

                deathUI.PlayerDead();
            }
            else //else if player is alive still
            {
                playerSprite.GetComponent<Animator>().SetTrigger("Hurt"); //play hurt animation

                slashButton.interactable = true;
                smashButton.interactable = true;
                blockButton.interactable = true;
                parryButton.interactable = true;
                healPotButton.interactable = true;
                healSkillButton.interactable = true;
            }

            isPlayer = true; //now it is players turn
        }
    }

    public void AttackButton()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        battleOptions.SetActive(false);
        attackOptions.SetActive(true);
    }

    public void DefendButton()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        battleOptions.SetActive(false);
        defendOptions.SetActive(true);
    }

    public void HealButton()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        battleOptions.SetActive(false);
        healOptions.SetActive(true);
    }

    public void BackAttack()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        attackOptions.SetActive(false);
        battleOptions.SetActive(true);
    }

    public void BackDefend()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        defendOptions.SetActive(false);
        battleOptions.SetActive(true);
    }

    public void BackHeal()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        healOptions.SetActive(false);
        battleOptions.SetActive(true);
    }

    public void Slash()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        Attack(false);

        if (!isEnemyDead)
        {
            StartCoroutine(EnemyAttack());
        }
    }

    public void Smash()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        if (playerStats.actionPoints == 0)
        {
            return;
        }

        playerStats.actionPoints -= 1;
        Attack(true);

        if(!isEnemyDead)
        {
            StartCoroutine(EnemyAttack());
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

        StartCoroutine(EnemyAttack());
    }

    public void Parry()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        playerStats.isBlocking = true; //player is blocking
        isParrying = true;

        playerStats.actionPoints -= 1;

        playerSprite.GetComponent<Animator>().SetTrigger("Block"); //play block animation
        playerSprite.GetComponent<Animator>().SetBool("IdleBlock", true); //play idle block animation

        StartCoroutine(EnemyAttack());
    }

    public void HealingPotion()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        if(playerStats.healthPotions == 0)
        {
            return;
        }

        playerStats.healthPotions -= 1;
        playerStats.health += 50;

        if(playerStats.health > playerStats.maxHealth)
        {
            playerStats.health = playerStats.maxHealth;
        }

        StartCoroutine(EnemyAttack());
    }

    public void HealingSpell()
    {
        if (isPlayerDead | isEnemyDead) //if player or enemy is dead
        {
            return;
        }

        if(playerStats.actionPoints == 0)
        {
            return;
        }

        playerStats.actionPoints -= 1;
        playerStats.health += 50;

        if (playerStats.health > playerStats.maxHealth)
        {
            playerStats.health = playerStats.maxHealth;
        }

        StartCoroutine(EnemyAttack());
    }

    public void ResetEnemy()
    {
        enemy.transform.Find(enemyAI.enemyName).gameObject.SetActive(false); //hide enemy

        roomType.GenerateRooms(); //generate new rooms

        enemyAI.ResetEnemy(); //reset enemy
    }

    public void ResetCombatUI()
    {
        battleOptions.SetActive(true);
        attackOptions.SetActive(false);
        defendOptions.SetActive(false);
        healOptions.SetActive(false);
    }

    public GameObject GetSprites(bool isPlayerSprite)
    {
        enemyName = enemyAI.enemyName;

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
            GameObject enemySprite = enemy.transform.Find(enemyName).gameObject; //get current enemy sprite

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

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(1);

        isPlayer = false;

        Attack(false);

        yield return new WaitForSeconds(1);

        if(isParrying)
        {
            Attack(false);
            isPlayer = true;
        }

        yield return new WaitForSeconds(1);
    }

    IEnumerator EnemyDeathTime()
    {
        yield return new WaitForSeconds(1);

        Debug.Log("test");
    }
}
