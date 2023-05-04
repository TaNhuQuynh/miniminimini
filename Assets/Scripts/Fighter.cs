using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitPonit = 10;
    public int maxHitPonit = 10;
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //push
    protected Vector3 pushDirection;

    //all fughters can receiveDammage/ die
    protected virtual void ReceiveDamage(Damage dam)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPonit -= dam.damageAmount;
            pushDirection = (transform.position - dam.origin).normalized * dam.pushForce;

            GameManager.instance.Showtext(dam.damageAmount.ToString(), 30, color: Color.red, transform.position, Vector3.zero, 0.5f);
            
            if (hitPonit <= 0)
            {
                hitPonit = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}

