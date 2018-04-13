using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public Button instructions;
    public Button play;
    public Button quit;
    public Button close;
    public Button credits;
    public Button menu;

    private GameObject instructionPane;

    private bool instructionsClicked;

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            instructionPane = GameObject.FindGameObjectWithTag("ShowOnClick");
            hideText();
        }
    }

    void hideText()
    {
        instructionPane.SetActive(false);
    }

    void showText()
    {
        instructionPane.SetActive(true);
    }

    public void Instructions()
    {
        showText();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credit");
    }

    public void Close()
    {
        hideText();
    }

    public void Play()
    {
        SceneManager.LoadScene("Test");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
