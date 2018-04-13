using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text scoreText;
    public Text scrapText;
    public Text waveText;
    public Text defenseText;

    public Button close;

    private GameObject instructionPane;

    // Use this for initialization
    void Start ()
    {
        instructionPane = GameObject.FindGameObjectWithTag("HideOnClick");
        scoreText.text = "SCORE: ";
        scrapText.text = "SCRAP: ";
        waveText.text = "WAVE: ";
        defenseText.text = "DEFENSE: ";
    }

    // Update is called once per frame
    void Update ()
    {
        //waveText.text = GameController.Wave.name;
        scrapText.text = "SCRAP: " + Player.scrap;

        if (Player.towerSelect == 0)
        {
            defenseText.text = "DEFENSE: Single Shot Tower";
        }
        if (Player.towerSelect == 1)
        {
            defenseText.text = "DEFENSE: Spread Shot Tower";
        }
        if (Player.towerSelect == 2)
        {
            defenseText.text = "DEFENSE: Lightning Trap";
        }
    }

    void hideText()
    {
        instructionPane.SetActive(false);
    }

    public void Close()
    {
        hideText();
    }
}
