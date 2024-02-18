using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainingButtons : MonoBehaviour
{
    public GameObject player;

    private PlayerStats playerStats;
    private ChangeUI changeUI;
    private RoomType roomType;

    private void Start()
    {
        playerStats = this.GetComponent<PlayerStats>();
        changeUI = this.GetComponent<ChangeUI>();
        roomType = this.GetComponent<RoomType>();
    }

    public void TrainAttack()
    {
        if(playerStats.actionPoints == 0)
        {
            Debug.Log("no action points mate");
            return;
        }

        playerStats.actionPoints -= 1;

        playerStats.attack += 3;

        playerStats.depth += 1;

        roomType.GenerateRooms();

        changeUI.ToggleUI(0);
    }

    public void TrainDefence()
    {
        if (playerStats.actionPoints == 0)
        {
            Debug.Log("no action points mate");
            return;
        }

        playerStats.actionPoints -= 1;

        playerStats.defence += 3;

        playerStats.depth += 1;

        roomType.GenerateRooms();

        changeUI.ToggleUI(0);
    }

    public void TrainHealth()
    {
        if (playerStats.actionPoints == 0)
        {
            Debug.Log("no action points mate");
            return;
        }

        playerStats.actionPoints -= 1;

        playerStats.maxHealth += 20;
        playerStats.health += 20;

        playerStats.depth += 1;

        roomType.GenerateRooms();

        changeUI.ToggleUI(0);
    }

    public void Rest()
    {
        playerStats.actionPoints += 1;
        playerStats.health += 10;

        if(playerStats.health > playerStats.maxHealth)
        {
            playerStats.health = playerStats.maxHealth;
        }

        playerStats.depth += 1;

        roomType.GenerateRooms();

        changeUI.ToggleUI(0);
    }
}
