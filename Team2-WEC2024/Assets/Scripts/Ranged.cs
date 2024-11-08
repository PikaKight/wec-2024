using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Collidable
{
    public int damage;
    public float speed = 10f;
    public float lifeTime = 3f;
    public float angle;
    public string shooter;

    private Rigidbody2D rb;

    // Start is called before the first frame update
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
            switch (shooter){
                case "Player":
                    EnemyController enemyController = coll.GetComponent<EnemyController>();

                    if (enemyController != null)
                    {
                        enemyController.changeHealth(damage * -1);   
                    }
                    break;

                case "Boss":
                    PlayerControl playerControl = coll.GetComponent<PlayerControl>();
                    if (playerControl != null)
                    {
                        playerControl.changeHealth(damage * -1);
                    }
                    break;

                default:
                    return;
            }
            
            Destroy(gameObject);
        }
    }
}
