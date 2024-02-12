using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {

    }

    public void ExitGame()
    {
        Debug.Log("Game closed..."); //for testing
        Application.Quit(); //close game
    }
}
