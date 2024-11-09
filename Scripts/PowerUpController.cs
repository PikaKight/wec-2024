
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PlayerControl playerControl;  // Reference to the PlayerControl script
    
    // Activate unlimited ammo
    public void ActivateUnlimitedAmmo(float duration)
    {
        playerControl.SetUnlimitedAmmo(true);
        Invoke("DeactivateUnlimitedAmmo", duration);
    }

    private void DeactivateUnlimitedAmmo()
    {
        playerControl.SetUnlimitedAmmo(false);
    }

    // Activate unlimited health
    public void ActivateUnlimitedHealth(float duration)
    {
        playerControl.SetInvincible(true);
        Invoke("DeactivateUnlimitedHealth", duration);
    }

    private void DeactivateUnlimitedHealth()
    {
        playerControl.SetInvincible(false);
    }

    // Clear all enemies and asteroids
    public void ClearScreen()
    {
        playerControl.ClearScreen();
    }
}
