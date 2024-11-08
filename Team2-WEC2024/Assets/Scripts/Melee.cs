using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Melee : Collidable
{
    public int damagePoint = 2;
    public float pushForce = 2.0f;

    public Transform attackPoint;

    public InputAction attackAction;
    public Animator attackAnimation;


    float cooldown = 0.05f;
    float lastSwing;

    bool isAttack = false;

    protected override void Start()
    {
        base.Start();
        attackAction.Enable();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        

        if (attackAction.WasPressedThisFrame()) {

            gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

            if (Time.time - lastSwing > cooldown) {

                isAttack = true;
            }
        }
        else
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            isAttack = false;
        }
        
    }

    void FixedUpdate()
    {
        if (isAttack)
        {
            attackAnimation.SetTrigger("Attack");
        }
        else
        {
            attackAnimation.Play("MeleeIdle");
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name != "Player")
        {
            Debug.Log("Attacked: " + coll.name);

            EnemyController enemy = coll.GetComponent<EnemyController>();

            if (enemy != null) { 
                enemy.changeHealth(damagePoint*-1);
            }

        }
    }
}
