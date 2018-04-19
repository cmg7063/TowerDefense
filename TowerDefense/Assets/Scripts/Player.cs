using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float speed;
    public GameObject tower1, tower2, tower3, tower4;
    private GameObject[] towers;
    private GameObject towerCurrent;
    public static int towerSelect;
    public static int scrap;

    public int score;

    public static int health;
    public bool iFrames;
    public float iFrameTime;
    public float iTimer;

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
        //towers[3] = tower4;
        towerCurrent = tower1;
        towerSelect = 0;
        health = 100;
        iFrames = false;
        iFrameTime = 5.0f;
        iTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
            
        }

        if(iFrames == true)
        {
            iTimer += 0.1f;
            Color tmp = this.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.3f;
            this.GetComponent<SpriteRenderer>().color = tmp;

            if (iTimer >= iFrameTime)
            {
                tmp.a = 1f;
                this.GetComponent<SpriteRenderer>().color = tmp;
                iFrames = false;
                iTimer = 0.0f;
            }
        }
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Scrap")
        {
            Destroy(collision.gameObject);
            scrap += 25;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (iFrames == false)
            {
                health -= 20;
                iFrames = true;
            }
            Debug.Log("collision with enemy. Health: " + health);
        }
    }
}
