using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void ExitGame()
    {
        Debug.Log("Closing game..."); //for testing
        Application.Quit(); //quit game
    }
}
