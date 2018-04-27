using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject tower1, tower2, tower3, tower4, tower5, tower6;
    private GameObject[] towers;
    private GameObject towerCurrent;
    public static int towerSelect;

    private Animator myAnimator;
    private Rigidbody2D myRigidbody;

    public int score;

    public static int health;
	public static int scrap;
	public float speed;
	public bool validLocation;

	public bool facingLeft;
    private bool facingRight;

    public bool iFrames;
    public float iFrameTime;
    public float iTimer;

    public int Score {
        get { return score; }
        set { score = value; }
    }

    // Use this for initialization
	void Start () {
        towers = new GameObject[6];
        towers[0] = tower1;
        towers[1] = tower2;
        towers[2] = tower3;
		towers[3] = tower4;
		towers[4] = tower5;
        towers[5] = tower6;

        towerCurrent = tower1;
        towerSelect = 0;

        health = 100;
		scrap = 100;

		facingLeft = false;
        facingRight = true;

        iFrames = false;
        iFrameTime = 5.0f;
        iTimer = 0.0f;

        myAnimator = GetComponent <Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (health <= 0) {
            SceneManager.LoadScene("GameOver"); 
        }

        float horizontal = Input.GetAxis("Horizontal");

		PlayerHit ();
		InputHandler (horizontal);

        Flip(horizontal);
    }

    void Flip(float horizontal) {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
            facingRight = !facingRight;
            Vector3 thisScale = transform.localScale;
            thisScale.x *= -1;
            transform.localScale = thisScale;
        }
    }

	void PlayerHit() {
		if (iFrames == true) {
			iTimer += 0.1f;
			Color tmp = this.GetComponent<SpriteRenderer>().color;
			tmp.a = 0.3f;
			this.GetComponent<SpriteRenderer>().color = tmp;

			if (iTimer >= iFrameTime) {
				tmp.a = 1f;
				this.GetComponent<SpriteRenderer>().color = tmp;
				iFrames = false;
				iTimer = 0.0f;
			}
		}
	}

    void InputHandler(float horizontal) {
        // Movement Inputs WASD
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A)) {
			facingLeft = true;
			transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
			facingLeft = false;
			transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        myAnimator.SetFloat("moveSpeed", Mathf.Abs(horizontal));

        // Tower Inputs
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            myAnimator.SetBool("buildKey", true);
            towerSelect += 1;
			if (towerSelect >= towers.Length) {
				towerSelect = 0;
			}

			towerCurrent = towers[towerSelect];
		}
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            myAnimator.SetBool("buildKey", true);
            towerSelect += 3;
            if (towerSelect >= towers.Length)
            {
                towerSelect -= 6;
            }

            towerCurrent = towers[towerSelect];
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            myAnimator.SetBool("buildKey", true);
            towerSelect -= 1;
            if (towerSelect < 0)
            {
                towerSelect = towers.Length - 1;
            }

            towerCurrent = towers[towerSelect];
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            myAnimator.SetBool("buildKey", true);
            towerSelect -= 3;
            if (towerSelect < 0)
            {
                towerSelect += 6;
            }

            towerCurrent = towers[towerSelect];
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            PlaceTower();
            myAnimator.SetBool("buildKey", false);
        }
    }

    void PlaceTower() {
		int scrapCost = towerCurrent.GetComponent<Buildable> ().scrapCost;

		if (scrap >= scrapCost && validLocation) {
			Vector3 position = gameObject.transform.position;
			float xOffset = 1.0f;
			if (facingLeft) {
				xOffset = -1.0f;
			}

			position.x = Mathf.Floor (position.x) + 0.5f + xOffset;
			position.y = Mathf.Floor (position.y) + 0.5f;
			position.z = -2;

			Instantiate(towerCurrent, position, gameObject.transform.rotation);
			scrap -= scrapCost;
        }
    }

	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Scrap") {
//			collider.gameObject.GetComponent<>(Enemy).scrapValue += 25;
			scrap += 25;

			Destroy(collider.gameObject);
        }
        if(collider.gameObject.tag == "EnemyBullet")
        {
            if (iFrames == false)
            {
                health -= 10;
                iFrames = true;
            }
            Debug.Log("collision with enemy bullet. Health: " + health);
        }
    }

	private void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.tag == "Enemy") {
            if (iFrames == false) {
                health -= 20;
                iFrames = true;
            }

            Debug.Log("collision with enemy. Health: " + health);
        }
    }
}
