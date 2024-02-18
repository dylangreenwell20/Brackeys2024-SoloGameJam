using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathUI : MonoBehaviour
{
    [SerializeField] private GameObject combatTopUI;
    [SerializeField] private GameObject combatUI;
    [SerializeField] private GameObject doorTopUI;
    [SerializeField] private GameObject doorUI;
    [SerializeField] private GameObject deathUI;
    [SerializeField] private TextMeshProUGUI depth;

    public CombatButtons combatButtons;
    public PlayerStats playerStats;

    public void PlayerDead()
    {
        deathUI.SetActive(true);
        depth.text = ("Depth: " + playerStats.depth);
    }

    public void Retry()
    {
        combatButtons.ResetCombatUI();
        combatTopUI.SetActive(false);
        combatUI.SetActive(false);

        combatButtons.ResetEnemy();

        deathUI.SetActive(false);

        playerStats.ResetStats();

        doorTopUI.SetActive(true);
        doorUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
