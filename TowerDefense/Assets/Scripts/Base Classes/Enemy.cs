using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    private EnemyState state = EnemyState.Wander;

    public float minDistance;
    public float maxDistance;

    public float speed;

    public enum EnemyState
    {
        Wander,
        Pursue,
        Stop
    }

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
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
            state = EnemyState.Wander;
        }
        else if (distance > 0)
        {
            state = EnemyState.Stop;
        }

        // State logic
        if (state == EnemyState.Pursue)
        {
            Debug.Log("Pursuing!");
            speed = 2.0f;
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (state == EnemyState.Wander)
        {
            Debug.Log("Wandering!");
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
        }
        if(state == EnemyState.Stop)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
        }

        //float angle = Mathf.Atan2(vecToPlayer.y, vecToPlayer.x) * Mathf.Rad2Deg - 90f;
        //Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, 180);
        //transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "TowerBullet" || collision.gameObject.tag == "Trap")
        {
            Debug.Log("Colliding with bullet");
            Destroy(gameObject);
        }
    }

}
