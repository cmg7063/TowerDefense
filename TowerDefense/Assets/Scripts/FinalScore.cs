using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour {

    public Text fScoreText;
    public static int fScore;

    // Use this for initialization
    void Start () {
        fScoreText.text = "FINAL SCORE: ";
        fScore = PlayerPrefs.GetInt("Score");
	}
	
	// Update is called once per frame
	void Update () {
        fScoreText.text = "FINAL SCORE: " + fScore;
	}
}
