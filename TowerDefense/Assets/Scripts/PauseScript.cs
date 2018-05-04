using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    public bool paused;
    GameObject[] pauseObjects;

    // Use this for initialization
    void Start () {
        paused = false;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }
        if (paused)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (!paused)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Resume()
    {
        Time.timeScale = 1;
        paused = !paused;
        hidePaused();
    }
    public void Play()
    {
        SceneManager.LoadScene("SungTest");
    }
    public void Info()
    {
        SceneManager.LoadScene("InfoScreen");
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }
}
