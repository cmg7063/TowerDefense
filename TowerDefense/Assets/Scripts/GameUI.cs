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
    public Text healthText;

    public Button close;

    private GameObject instructionPane;

    public Image box1;
    public Image box2;
    public Image box3;
    public Image box4;
	public Image box5;
    public Image box6;

    public static int scoreTotal;

    // Use this for initialization
    void Start ()
    {
        instructionPane = GameObject.FindGameObjectWithTag("HideOnClick");
        scoreText.text = "SCORE:\n";
        scrapText.text = "SCRAP: ";
        waveText.text = "WAVE: ";
        defenseText.text = "DEFENSE: ";
        healthText.text = "HEALTH: ";
        scoreTotal = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        //waveText.text = GameController.Wave.name;
        scrapText.text = "SCRAP: " + Player.scrap;
        healthText.text = "HEALTH: " + Player.health;
        scoreText.text = "SCORE:" + scoreTotal;

		if (Player.towerSelect == 0) {
			defenseText.text = "DEFENSE: Single Shot Tower\nShoots a single, fast bullet.\nScrap Cost: 25";
            box6.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box1.GetComponent<Image>().color = new Color32(200, 200, 200, 100);
            box2.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box3.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box4.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box5.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
        }
        else if (Player.towerSelect == 1) {
			defenseText.text = "DEFENSE: Spread Shot Tower\nShoots three bullets in spread.\nScrap Cost: 50";
            box1.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box2.GetComponent<Image>().color = new Color32(200, 200, 200, 100);
            box3.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box4.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box5.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
        } else if (Player.towerSelect == 2) {
			defenseText.text = "DEFENSE: Pulse Tower\nShoots eight bullets around tower.\nScrap Cost: 75";
            box2.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box3.GetComponent<Image>().color = new Color32(200, 200, 200, 100);
            box4.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box5.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box6.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
        } else if (Player.towerSelect == 3) {
			defenseText.text = "DEFENSE: Lightning Trap\nSmall damage to enemies walking on it.\nScrap Cost: 25";
            box3.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box4.GetComponent<Image>().color = new Color32(200, 200, 200, 100);
            box5.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box6.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box1.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
        } else if (Player.towerSelect == 4) {
			defenseText.text = "DEFENSE: Flame Trap\nShoots fire four ways around trap.\nScrap Cost: 50";
            box4.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box5.GetComponent<Image>().color = new Color32(200, 200, 200, 100);
            box6.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box1.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box2.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
        } else if (Player.towerSelect == 5) {
			defenseText.text = "DEFENSE: Laser Trap\nFires piercing laser.\nScrap Cost: 75";
			box5.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box6.GetComponent<Image>().color = new Color32(200, 200, 200, 100);
            box1.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box2.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
            box3.GetComponent<Image>().color = new Color32(146, 146, 146, 100);
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
