using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // damage struct
    public int[] damagePont = { 1, 2, 3, 4, 5, 6, 7};
    public float[] pushForce = { 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f };

    //upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

   

    protected override void Start()
    {
        base.Start();
        
        anim = GetComponent<Animator>();
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
                damageAmount = damagePont[weaponLevel],
                origin=transform.position,
                pushForce=pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dam);
        }
    }

    private void Swing()
    {
        Debug.Log("Swing");

        anim.SetTrigger("Swing");

    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprite[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprite[weaponLevel];
    }
}
