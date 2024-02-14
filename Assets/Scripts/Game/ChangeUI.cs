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

    [SerializeField] private TextMeshProUGUI healthTextDoors; //reference to health text in doors UI
    [SerializeField] private TextMeshProUGUI depthTextDoors; //reference to depth text in doors UI
    [SerializeField] private TextMeshProUGUI attackTextDoors; //reference to attack text in doors UI
    [SerializeField] private TextMeshProUGUI defenceTextDoors; //reference to defence text in doors UI
    [SerializeField] private TextMeshProUGUI actionPointTextDoors; //reference to action point text in doors UI

    [SerializeField] private TextMeshProUGUI healthTextBattle; //reference to health text in battle UI
    [SerializeField] private TextMeshProUGUI actionPointTextBattle; //reference to action point text in battle UI

    [SerializeField] private bool inBattle; //bool to check if player is in battle or not

    private PlayerStats playerStats; //reference to player stats
    private EnemyAI enemyStats; //reference to enemy stats

    private void Start()
    {
        inBattle = true; //start with "in battle" state so i can toggle to the correct ui (load the door screen, hide battle screen)

        playerStats = this.GetComponent<PlayerStats>();
        enemyStats = this.GetComponent<EnemyAI>();

        ToggleUI(); //toggle ui to show correct ui/screen
        UpdateStatsDoorsUI(); //update door stats UI
        UpdateStatsBattleUI(); //update battle stats UI
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
    }
}
