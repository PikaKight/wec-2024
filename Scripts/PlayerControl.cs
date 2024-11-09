using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;



public class PlayerControl : MonoBehaviour
{
    public float speed;
    public InputAction moveAction;
    public InputAction pauseAction;
    public float maxHealth;
    public float timeInvincible;

    public InputAction shootAction;
    public Transform firingPoint;
    public float fireRate = 0.5f;
    public GameObject bullet;
    public int rangedDamage = 3;
    float fireTimer;

    public TextMeshProUGUI healthStatus;


    Rigidbody2D rb;
    Vector2 move;
    bool isInvincible;
    float damageCooldown;
    bool isFacingRight = true;
    public float health { get { return currentHealth; } }

    float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        moveAction.Enable();

        pauseAction.Enable();

        shootAction.Enable();

        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = moveAction.ReadValue<Vector2>();

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <= 0)
            {
                isInvincible = false;
            }
        }

        if (pauseAction.WasPressedThisFrame())
        {
            Pause.isPaused = !Pause.isPaused;
            Debug.Log("Pressed: " + Pause.isPaused);
        }

        if (shootAction.WasPressedThisFrame() && fireTimer <= 0)
        {
            GameObject bullet1 = Instantiate(bullet, firingPoint.position, firingPoint.rotation);

            Ranged b1Data = bullet1.GetComponent<Ranged>();

            b1Data.damage = rangedDamage;

            b1Data.shooter = gameObject.name;

            fireTimer = fireRate;
        }

        fireTimer -= Time.deltaTime;

        if (currentHealth <= 0)
        {
            Vector2 position = transform.position;

            position.x = -2.56f;
            position.y = 1.008f;
            transform.position = position;

            currentHealth = maxHealth;

            healthStatus.text = currentHealth.ToString() + " HP";

        }
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + (move * speed * Time.deltaTime);


        rb.MovePosition(position);
    }

    public void changeHealth(int health)
    {
        if (health < 0)
        {
            if (isInvincible)
            {
                return;
            }

            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
        healthStatus.text = currentHealth.ToString() + " HP";
    }
}
