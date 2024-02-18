using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour
{
    [SerializeField] private GameObject doorScreenUI; //reference to door screen ui
    [SerializeField] private GameObject battleScreenUI; //reference to battle screen ui
    [SerializeField] private GameObject doorScreen; //reference to door screen and sprites in it
    [SerializeField] private GameObject battleScreen; //reference to battle screen and sprites in it
    [SerializeField] private GameObject enemyPositons; //reference to enemy positions object with enemy sprites in it

    [SerializeField] private TextMeshProUGUI healthTextDoors; //reference to health text in doors UI
    [SerializeField] private TextMeshProUGUI depthTextDoors; //reference to depth text in doors UI
    [SerializeField] private TextMeshProUGUI attackTextDoors; //reference to attack text in doors UI
    [SerializeField] private TextMeshProUGUI defenceTextDoors; //reference to defence text in doors UI
    [SerializeField] private TextMeshProUGUI actionPointTextDoors; //reference to action point text in doors UI

    [SerializeField] private TextMeshProUGUI healthTextBattle; //reference to health text in battle UI
    [SerializeField] private TextMeshProUGUI actionPointTextBattle; //reference to action point text in battle UI
    [SerializeField] private TextMeshProUGUI enemyNameText; //reference to enemy name text in battle UI
    [SerializeField] private TextMeshProUGUI enemyHealthText; //reference to enemy health text in battle UI

    [SerializeField] private bool inBattle; //bool to check if player is in battle or not
    [SerializeField] private bool leftDoorOpened; //bool to check if player opened left door
    [SerializeField] private bool rightDoorOpened; //bool to check if player opened right door

    private PlayerStats playerStats; //reference to player stats
    private EnemyAI enemyStats; //reference to enemy stats
    private RoomType roomType; //reference to room type

    private void Start()
    {
        inBattle = true; //start with "in battle" state so i can toggle to the correct ui (load the door screen, hide battle screen)

        playerStats = this.GetComponent<PlayerStats>();
        enemyStats = this.GetComponent<EnemyAI>();
        roomType = this.GetComponent<RoomType>();

        ToggleUI(); //toggle ui to show correct ui/screen
        UpdateStatsDoorsUI(); //update door stats UI
        UpdateStatsBattleUI(); //update battle stats UI
    }

    public void LeftDoorOpened()
    {
        leftDoorOpened = true;
        ToggleUI();
        leftDoorOpened = false;
    }

    public void RightDoorOpened()
    {
        rightDoorOpened = true;
        ToggleUI();
        rightDoorOpened = false;
    }

    public void ToggleUI()
    {
        if(inBattle)
        {
            inBattle = false; //player no longer in battle
            
            battleScreenUI.SetActive(false); //hide battle screen ui
            battleScreen.SetActive(false); //hide battle screen

            doorScreenUI.SetActive(true); //show door screen ui
            doorScreen.SetActive(true); //show door screen

            UpdateStatsDoorsUI(); //update player stats ui on door screen
            
        }
        else
        {
            inBattle = true; //player now in battle

            doorScreenUI.SetActive(false); //hide door screen ui
            doorScreen.SetActive(false); //hide door screen

            //get what door was clicked on

            if(leftDoorOpened)
            {
                //get room type



                //display ui for that room type



                //combat room

                UpdateStatsBattleUI(); //update battle ui

                battleScreenUI.SetActive(true); //show battle screen ui
                battleScreen.SetActive(true); //show battle screen

                //spawn correct enemy

                enemyPositons.transform.Find(enemyStats.enemyName).gameObject.SetActive(true); //for testing enemy spawn
            }
            else if(rightDoorOpened)
            {
                //get room type



                //display ui for that room type



                //combat room

                UpdateStatsBattleUI(); //update battle ui

                battleScreenUI.SetActive(true); //show battle screen ui
                battleScreen.SetActive(true); //show battle screen

                //spawn correct enemy

                enemyPositons.transform.Find(enemyStats.enemyName).gameObject.SetActive(true); //for testing enemy spawn
            }
        }
    }

    public void WhatRoomType()
    {
        RoomTypeList leftDoor = roomType.leftDoor;
        RoomTypeList rightDoor = roomType.rightDoor;

        //which rooms to display logic

        if (leftDoor == RoomTypeList.Empty)
        {
            //generate empty room
        }
        else if (leftDoor == RoomTypeList.Chest)
        {
            //generate chest room
        }
        else
        {
            //generate combat room
        }

        if (rightDoor == RoomTypeList.Empty)
        {
            //generate empty room
        }
        else if (rightDoor == RoomTypeList.Chest)
        {
            //generate chest room
        }
        else
        {
            //generate combat room
        }
    }

    public void ChestOpened()
    {

    }

    public void UpdateStatsDoorsUI()
    {
        healthTextDoors.text = ("Health: " + playerStats.health + "/" + playerStats.maxHealth); //update text box to show health values
        depthTextDoors.text = ("Depth: " + playerStats.depth);
        attackTextDoors.text = ("Attack: " + playerStats.attack);
        defenceTextDoors.text = ("Defence: " + playerStats.defence);
        actionPointTextDoors.text = ("Action Points: " + playerStats.actionPoints);
    }

    public void UpdateStatsBattleUI()
    {
        healthTextBattle.text = ("Health: " + playerStats.health + "/" + playerStats.maxHealth); //update text box to show health values
        actionPointTextBattle.text = ("Action Points: " + playerStats.actionPoints);
        enemyNameText.text = (enemyStats.enemyName);
        enemyHealthText.text = (enemyStats.health + "/" +  enemyStats.maxHealth);
    }
}
