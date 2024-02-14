using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsButton : MonoBehaviour
{
    [SerializeField] private GameObject statsMenu; //reference to stats menu
    [SerializeField] private bool statsActive; //bool to check if stat menu is active or not

    private void Start()
    {
        statsMenu.SetActive(false); //hide stat menu on start
        statsActive = false; //stats are not shown
    }

    public void ToggleStats()
    {
        if (statsActive) //if stat menu is currently shown
        {
            statsMenu.SetActive(false); //hide menu
            statsActive=false; //stats are now hidden
        }
        else
        {
            statsMenu.SetActive(true); //show stat menu
            statsActive=true; //stats are now shown
        }
    }
}
