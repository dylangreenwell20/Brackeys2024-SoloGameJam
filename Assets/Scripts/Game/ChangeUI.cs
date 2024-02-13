using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUI : MonoBehaviour
{
    [SerializeField] private GameObject doorScreenUI; //reference to door screen ui
    [SerializeField] private GameObject battleScreenUI; //reference to battle screen ui
    [SerializeField] private GameObject doorScreen; //reference to door screen and sprites in it
    [SerializeField] private GameObject battleScreen; //reference to battle screen and sprites in it

    [SerializeField] private bool inBattle; //bool to check if player is in battle or not

    private void Start()
    {
        inBattle = true; //start with "in battle" state so i can toggle to the correct ui (load the door screen, hide battle screen)

        ToggleUI(); //toggle ui to show correct ui/screen
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
            
        }
        else
        {
            inBattle = true; //player now in battle

            doorScreenUI.SetActive(false); //hide door screen ui
            doorScreen.SetActive(false); //hide door screen

            battleScreenUI.SetActive(true); //show battle screen ui
            battleScreen.SetActive(true); //show battle screen
        }
    }
}
