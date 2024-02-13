using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUI : MonoBehaviour
{
    [SerializeField] private GameObject doorScreenUI; //reference to door screen ui
    [SerializeField] private GameObject battleScreenUI; //reference to battle screen ui
    [SerializeField] private GameObject doors; //reference to doors in door screen
    [SerializeField] private GameObject playerDoors; //reference to player sprite in door screen

    [SerializeField] private bool inBattle; //bool to check if player is in battle or not

    private void Start()
    {
        doorScreenUI.SetActive(true); //show door screen ui - game starts on door screen
        battleScreenUI.SetActive(false); //hide battle screen ui

        inBattle = false; //player is not in battle
    }

    public void ToggleUI()
    {
        if(inBattle)
        {
            inBattle = false; //player no longer in battle
            doorScreenUI.SetActive(true); //show door screen ui
            battleScreenUI.SetActive(false); //hide battle screen ui
            doors.SetActive(true); //show doors
            playerDoors.SetActive(true); //show player sprite in door room

            //hide player and enemies in battle room
        }
        else
        {
            inBattle = true; //player now in battle
            doorScreenUI.SetActive(false); //hide door screen ui
            battleScreenUI.SetActive(true); //show battle screen ui
            doors.SetActive(false); //hide doors
            playerDoors.SetActive(false); //hide player sprite in door room

            //show player and enemies in battle room
        }
    }
}
