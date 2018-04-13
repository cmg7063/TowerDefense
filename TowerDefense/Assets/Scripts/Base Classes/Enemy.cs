using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    private EnemyState state = EnemyState.Look;

    public float health;

    public float minDistance;
    public float maxDistance;

    public float speed;

    public enum EnemyState
    {
        Look,
        Pursue,
        Stop
    }

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(health < 0)
        {
            Destroy(gameObject);
            // add scrap
        }

        Vector2 vecToPlayer = (player.transform.position - transform.position);
        float distance = Vector2.Distance(player.transform.position, transform.position);

        float angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;
        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);

        if (distance > minDistance && distance < maxDistance)
        {
            state = EnemyState.Pursue;
        }
        else if(distance > maxDistance)
        {
            state = EnemyState.Look;
        }
        else if (distance > 0)
        {
            state = EnemyState.Stop;
        }

        // State logic
        if (state == EnemyState.Pursue)
        {
            //Debug.Log("Pursuing");
            speed = 2.0f;
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (state == EnemyState.Look)
        {
            //Debug.Log("Looking");
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
        }
        if(state == EnemyState.Stop)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "TowerBullet")
        {
            health -= 20;
            Destroy(collider.gameObject);
            Debug.Log("Taking damage from bullet. Enemy health: " + health);
        }
        if(collider.gameObject.tag == "Trap")
        {
            health -= 50;
            Destroy(collider.gameObject);
            Debug.Log("Taking damage from trap. Enemy health: " + health);
        }
    }

}
