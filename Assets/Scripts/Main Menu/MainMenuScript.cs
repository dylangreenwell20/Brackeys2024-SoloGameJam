using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; //reference to main menu
    [SerializeField] private GameObject optionsMenu; //reference to options menu

    [SerializeField] private Animator transition; //reference to transition animation
    [SerializeField] private float transitionTime; //reference to transition animation time

    private void Start()
    {
        mainMenu.SetActive(true); //show main menu
        optionsMenu.SetActive(false); //hide options menu
    }

    public void PlayGame()
    {
        Debug.Log("Game played..."); //for testing
        StartCoroutine(LoadLevel(1)); //load game scene
    }

    public void ExitGame()
    {
        Debug.Log("Game closed..."); //for testing
        Application.Quit(); //close game
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("StartGame"); //start menu animation

        yield return new WaitForSeconds(transitionTime); //wait for transition time (essentially wait for animation to finish)

        SceneManager.LoadScene(levelIndex); //load  game scene
    }

}
