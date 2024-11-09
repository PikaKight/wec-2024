using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Collidable
{
    public int damage = 1;  // Set to 1 to reduce health by one on hit
    public float speed = 10f;
    public float lifeTime = 3f;
    public float angle;
    public string shooter;

    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        rb.rotation = angle;
        rb.velocity = transform.up * speed;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name != shooter)
        {
            switch (shooter)
            {
                case "Player":
                    EnemyController enemyController = coll.GetComponent<EnemyController>();

                    if (enemyController != null)
                    {
                        enemyController.changeHealth(-damage);  // Decrease enemy health by 1
                    }
                    break;

                case "Boss":
                    PlayerControl playerControl = coll.GetComponent<PlayerControl>();
                    if (playerControl != null)
                    {
                        playerControl.changeHealth(-damage);
                    }
                    break;

                default:
                    return;
            }

            Destroy(gameObject);  // Destroy the bullet on hit
        }
    }
}
