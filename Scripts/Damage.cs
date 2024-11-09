using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerControl controller = other.GetComponent<PlayerControl>();


        if (controller != null)
        {
            controller.changeHealth(-1 * damage);
        }
    }
}
