using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;

public class ChangeUI : MonoBehaviour
{
    [SerializeField] private GameObject doorScreenUI; //reference to door screen ui
    [SerializeField] private GameObject battleScreenUI; //reference to battle screen ui
    [SerializeField] private GameObject emptyScreenUI; //reference to empty screen ui
    [SerializeField] private GameObject chestScreenUI; //reference to chest screen ui
    [SerializeField] private GameObject doorScreen; //reference to door screen and sprites in it
    [SerializeField] private GameObject battleScreen; //reference to battle screen and sprites in it
    [SerializeField] private GameObject emptyScreen; //reference to empty screen and sprites in it
    [SerializeField] private GameObject chestScreen; //reference to chest screen and sprites in it

    [SerializeField] private GameObject enemyPositons; //reference to enemy positions object with enemy sprites in it

    [SerializeField] private TextMeshProUGUI healthTextDoors; //reference to health text in doors UI
    [SerializeField] private TextMeshProUGUI depthTextDoors; //reference to depth text in doors UI
    [SerializeField] private TextMeshProUGUI attackTextDoors; //reference to attack text in doors UI
    [SerializeField] private TextMeshProUGUI defenceTextDoors; //reference to defence text in doors UI
    [SerializeField] private TextMeshProUGUI actionPointTextDoors; //reference to action point text in doors UI
    [SerializeField] private TextMeshProUGUI emptyRoomActionPointText; //reference to empty room action point text in top UI

    [SerializeField] private TextMeshProUGUI healthTextBattle; //reference to health text in battle UI
    [SerializeField] private TextMeshProUGUI actionPointTextBattle; //reference to action point text in battle UI
    [SerializeField] private TextMeshProUGUI enemyNameText; //reference to enemy name text in battle UI
    [SerializeField] private TextMeshProUGUI enemyHealthText; //reference to enemy health text in battle UI

    public Animator animator; //reference to chest animator

    [SerializeField] private bool leftDoorOpened; //bool to check if player opened left door
    [SerializeField] private bool rightDoorOpened; //bool to check if player opened right door

    private PlayerStats playerStats; //reference to player stats
    private EnemyAI enemyStats; //reference to enemy stats
    private RoomType roomType; //reference to room type

    private void Start()
    {
        playerStats = this.GetComponent<PlayerStats>();
        enemyStats = this.GetComponent<EnemyAI>();
        roomType = this.GetComponent<RoomType>();

        ToggleUI(0); //toggle ui to show correct ui/screen
        UpdateStatsDoorsUI(); //update door stats UI
        UpdateStatsBattleUI(); //update battle stats UI
    }

    public void LeftDoorOpened()
    {
        leftDoorOpened = true; //left door opened

        RoomTypeList leftDoor = roomType.leftDoor; //get door type

        int roomNumber = WhatRoomType(leftDoor); //get int of room index

        ToggleUI(roomNumber); //toggle ui with correct room index

        leftDoorOpened = false; //set to false
    }

    public void RightDoorOpened()
    {
        rightDoorOpened = true; //right door opened

        RoomTypeList rightDoor = roomType.rightDoor; //get door type

        int roomNumber = WhatRoomType(rightDoor); //get int of room index

        ToggleUI(roomNumber); //toggle ui with correct room index

        rightDoorOpened = false; //set to false
    }

    public void ToggleUI(int roomIndex)
    {
        if(roomIndex == 0) //door room
        {
            battleScreenUI.SetActive(false); //hide battle screen ui
            battleScreen.SetActive(false); //hide battle screen

            emptyScreenUI.SetActive(false);
            emptyScreen.SetActive(false);

            chestScreenUI.SetActive(false);
            chestScreen.SetActive(false);

            doorScreenUI.SetActive(true); //show door screen ui
            doorScreen.SetActive(true); //show door screen

            UpdateStatsDoorsUI(); //update player stats ui on door screen
        }
        else if(roomIndex == 1) //combat room
        {
            doorScreenUI.SetActive(false); //hide door screen ui
            doorScreen.SetActive(false); //hide door screen

            UpdateStatsBattleUI(); //update battle ui

            battleScreenUI.SetActive(true); //show battle screen ui
            battleScreen.SetActive(true); //show battle screen

            //spawn correct enemy

            enemyPositons.transform.Find(enemyStats.enemyName).gameObject.SetActive(true); //spawn enemy
        }
        else if(roomIndex == 2) //empty room
        {
            doorScreenUI.SetActive(false); //hide door screen ui
            doorScreen.SetActive(false); //hide door screen

            animator.SetTrigger("LoadRoom");

            emptyRoomActionPointText.text = ("Action Points: " + playerStats.actionPoints);

            emptyScreenUI.SetActive(true);
            emptyScreen.SetActive(true);
        }
        else if (roomIndex == 3) //chest room
        {
            doorScreenUI.SetActive(false); //hide door screen ui
            doorScreen.SetActive(false); //hide door screen

            animator.SetTrigger("ChestIdle");

            chestScreenUI.SetActive(true);
            chestScreen.SetActive(true);
        }
    }

    public int WhatRoomType(RoomTypeList door)
    {
        //'which rooms to display' logic

        if(door == RoomTypeList.Combat)
        {
            //generate combat room
            return 1;
        }
        else if (door == RoomTypeList.Empty)
        {
            //generate Empty room
            return 2;
        }
        else if (door == RoomTypeList.Chest)
        {
            //generate chest room
            return 3;
        }
        else
        {
            //error
            return -1;
        }
    }

    public void ChestOpened()
    {
        StartCoroutine(OpenChest()); //open chest co routine
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

    IEnumerator OpenChest()
    {
        animator.SetTrigger("ChestOpened"); //start chest open animation

        yield return new WaitForSeconds(2); //wait for transition time (essentially wait for animation to finish)

        playerStats.depth += 1; //increment depth
        playerStats.healthPotions += 1; //add health potion

        roomType.GenerateRooms(); //generate new rooms

        ToggleUI(0); //change ui to door room

        animator.SetTrigger("ChestOpenToIdle"); //go back to idle chest state
    }
}
