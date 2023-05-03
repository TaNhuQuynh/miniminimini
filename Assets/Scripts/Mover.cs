using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider2D;
    protected Vector3 move;
    protected RaycastHit2D hit;
    //public float speed;

    protected float ySpeed = 1.5f;
    protected float xSpeed = 2.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    
    protected virtual void UpdateMotor(Vector3 input)
    {
        //reset moveDelta
        
        move = new Vector3(input.x*xSpeed,input.y*ySpeed,0);

        //hoán đổi hướng sprite, cho dù đi sang trái hay phải
        if (move.x > 0)//right
        {
            transform.localScale = Vector3.one;
        }
        else if (move.x < 0)//left
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //đảm bảo rằng có thể di chuyển theo hướng này, bằng cách truyền một hộp ở đó trước, nếu hộp trả về null, thi có thể tự do di chuyển
        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(0, move.y), Mathf.Abs(move.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //làm vật di chuyển
            transform.Translate(0, move.y * Time.deltaTime, 0);
        }//X

        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(move.x, 0), Mathf.Abs(move.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //làm vật di chuyển
            transform.Translate(move.x * Time.deltaTime, 0, 0);
        }//Y
    }
}

