using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    [Header("Basic Stats")]
    public float speed = 5f;
    public float maxHealth = 100f;
    public float timeInvincible = 2f;

    [Header("Combat Stats")]
    public float baseFireRate = 0.5f;
    public int baseDamage = 3;
    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 1.5f;

    [Header("References")]
    public InputAction moveAction;
    public InputAction pauseAction;
    public InputAction shootAction;
    public InputAction reloadAction;
    public Transform firingPoint;
    public GameObject bullet;
    public TextMeshProUGUI healthStatus;
    public TextMeshProUGUI ammoStatus;

    [Header("Power Up Effects")]
    private float speedMultiplier = 1f;
    private float fireRateMultiplier = 1f;
    private float damageMultiplier = 1f;

    // Private variables
    private Rigidbody2D rb;
    private Vector2 move;
    private bool isInvincible;
    private float damageCooldown;
    private bool isFacingRight = true;
    private float currentHealth;
    private float fireTimer;
    private bool isReloading;
    private float reloadTimer;

    // Power up timers
    private float speedPowerUpTimer;
    private float fireRatePowerUpTimer;
    private float damagePowerUpTimer;

    void Start()
    {
        moveAction.Enable();
        pauseAction.Enable();
        shootAction.Enable();
        reloadAction.Enable();

        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    void Update()
    {
        move = moveAction.ReadValue<Vector2>();
        HandleInvincibility();
        HandlePause();
        HandleShooting();
        HandleReload();
        HandlePowerUpTimers();
        CheckHealth();
    }

    void HandleInvincibility()
    {
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <= 0)
            {
                isInvincible = false;
            }
        }
    }

    void HandlePause()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            Pause.isPaused = !Pause.isPaused;
        }
    }

    void HandleShooting()
    {
        if (shootAction.WasPressedThisFrame() && fireTimer <= 0 && !isReloading && currentAmmo > 0)
        {
            GameObject bulletInstance = Instantiate(bullet, firingPoint.position, firingPoint.rotation);
            Ranged bulletData = bulletInstance.GetComponent<Ranged>();
            bulletData.damage = Mathf.RoundToInt(baseDamage * damageMultiplier);
            bulletData.shooter = gameObject.name;

            currentAmmo--;
            fireTimer = baseFireRate / fireRateMultiplier;
            UpdateUI();
        }
        fireTimer -= Time.deltaTime;
    }

    void HandleReload()
    {
        if (reloadAction.WasPressedThisFrame() && !isReloading && currentAmmo < maxAmmo)
        {
            StartReload();
        }

        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                CompleteReload();
            }
        }
    }

    void StartReload()
    {
        isReloading = true;
        reloadTimer = reloadTime;
    }

    void CompleteReload()
    {
        isReloading = false;
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    void HandlePowerUpTimers()
    {
        if (speedPowerUpTimer > 0)
        {
            speedPowerUpTimer -= Time.deltaTime;
            if (speedPowerUpTimer <= 0)
            {
                speedMultiplier = 1f;
            }
        }

        if (fireRatePowerUpTimer > 0)
        {
            fireRatePowerUpTimer -= Time.deltaTime;
            if (fireRatePowerUpTimer <= 0)
            {
                fireRateMultiplier = 1f;
            }
        }

        if (damagePowerUpTimer > 0)
        {
            damagePowerUpTimer -= Time.deltaTime;
            if (damagePowerUpTimer <= 0)
            {
                damageMultiplier = 1f;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb.position + (move * speed * speedMultiplier * Time.deltaTime);
        rb.MovePosition(position);
    }

    public void ApplyPowerUp(PowerUpType type, float value, float duration)
    {
        switch (type)
        {
            case PowerUpType.Health:
                changeHealth(Mathf.RoundToInt(value));
                break;
            case PowerUpType.Speed:
                speedMultiplier = value;
                speedPowerUpTimer = duration;
                break;
            case PowerUpType.FireRate:
                fireRateMultiplier = value;
                fireRatePowerUpTimer = duration;
                break;
            case PowerUpType.Damage:
                damageMultiplier = value;
                damagePowerUpTimer = duration;
                break;
            case PowerUpType.AmmoBoost:
                currentAmmo = Mathf.Min(currentAmmo + Mathf.RoundToInt(value), maxAmmo);
                UpdateUI();
                break;
        }
    }

    public void changeHealth(int amount)
    {
        if (amount < 0 && isInvincible)
        {
            return;
        }

        if (amount < 0)
        {
            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateUI();
    }

    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Vector2 position = new Vector2(-2.56f, 1.008f);
            transform.position = position;
            currentHealth = maxHealth;
            currentAmmo = maxAmmo;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (healthStatus != null)
        {
            healthStatus.text = $"{currentHealth} HP";
        }
        if (ammoStatus != null)
        {
            ammoStatus.text = $"Ammo: {currentAmmo}/{maxAmmo}";
        }
    }
}