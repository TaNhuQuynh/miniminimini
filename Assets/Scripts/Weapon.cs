using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // damage struct
    public int damagePont = 1;
    public float pushRorce = 2.0f;

    //upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //swing
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
            {
                return;
            }

            //create a new damage obj, then we will send it to the fighter we've hit
            Damage dam = new Damage
            {
                damageAmount = damagePont,
                origin=transform.position,
                pushForce=pushRorce
            };

            coll.SendMessage("ReceiveDamage", dam);
        }
    }

    private void Swing()
    {
        Debug.Log("Swing");
    }
}
