using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public GameObject tower1, tower2, tower3;
    private GameObject[] towers;
    private GameObject towerCurrent;
    public static int towerSelect;
    public static int scrap;

    public int score;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    // Use this for initialization
    void Start () {
        speed = 3.0f;
        scrap = 100;
        towers = new GameObject[3];
        towers[0] = tower1;
        towers[1] = tower2;
        towers[2] = tower3;
        towerCurrent = tower1;
        towerSelect = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            towerSelect += 1;
            if(towerSelect >= towers.Length)
            {
                towerSelect = 0;
            }
            towerCurrent = towers[towerSelect];
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            PlaceTower();
        }
    }

    void PlaceTower()
    {
        if (scrap >= 50)
        {
            Instantiate(towerCurrent, this.transform.position, this.transform.rotation);
            scrap -= 50;
        }
    }
}
