using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Mover
{

    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(gameObject);
    }

    protected override void ReceiveDamage(Damage dam)
    {
        base.ReceiveDamage(dam);
        GameManager.instance.OnHitpointChange();
;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //Debug.Log(x);
        //Debug.Log(y);
        UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprite[skinId];
    }

    public void OnLevelUp()
    {
        maxHitPonit++;
        hitPonit = maxHitPonit;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
    public void Heal(int healingAmount)
    {
        if (hitPonit == maxHitPonit)
            return;

        hitPonit += healingAmount;
        if (hitPonit > maxHitPonit)
            hitPonit = maxHitPonit;
        GameManager.instance.Showtext("+" + healingAmount.ToString() + " hp", 25, color: Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }

}
   
