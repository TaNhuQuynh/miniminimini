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
}
   
