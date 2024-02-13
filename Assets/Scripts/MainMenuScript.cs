using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; //reference to main menu
    [SerializeField] private GameObject optionsMenu; //reference to options menu

    private void Start()
    {
        mainMenu.SetActive(true); //show main menu
        optionsMenu.SetActive(false); //hide options menu
    }

    public void PlayGame()
    {
        Debug.Log("Game played..."); //for testing
        SceneManager.LoadScene(1); //load game scene
    }

    public void ExitGame()
    {
        Debug.Log("Game closed..."); //for testing
        Application.Quit(); //close game
    }
}
