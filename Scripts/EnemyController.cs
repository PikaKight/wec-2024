using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float maxMoveTime;
    public bool verticle;
    public int damage;
    public bool range;
    public float firingRate = 0.5f;
    public GameObject bullet;

    Rigidbody2D rb;

    float moveTime;
    float wait = 5;
    float firingTimer;

    public int maxHealth;
    float currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveTime = maxMoveTime;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Debug.Log(gameObject.gameObject.name + " Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);  // Enemy is "dead"
        }

        if (range && firingTimer < 0)
        {
            GameObject bullet1 = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);

            Ranged b1Data = bullet1.GetComponent<Ranged>();
            b1Data.damage = damage;  // Bullet inherits enemy's damage
            b1Data.shooter = gameObject.name;
            b1Data.angle = 270;

            firingTimer = firingRate;
        }

        firingTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Vector2 position = rb.position;

        if (moveTime < 0)
        {
            speed *= -1;
            moveTime = maxMoveTime;
        }

        if (random && (wait < 0))
        {
            verticle = !verticle;
            wait = 5;
        }

        if (verticle)
        {
            position.y += speed * Time.deltaTime;
        }
        else
        {
            position.x += speed * Time.deltaTime;
        }

        rb.MovePosition(position);

        moveTime -= Time.deltaTime;
        wait -= Time.deltaTime;
    }

    public void changeHealth(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControl player = other.gameObject.GetComponent<PlayerControl>();

        if (player != null)
        {
            player.changeHealth(-damage);
        }
    }
}
