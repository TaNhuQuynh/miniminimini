using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    // damage
    public int damage=1;
    public float pushForce=5;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag=="Fighter"&& coll.name == "Player")
        {
            //create a new dam obj, before sending it to the player
            Damage dam = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", dam);
        }
    }
}
