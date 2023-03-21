using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public BoxCollider2D boxCollider2D;
    [SerializeField] public Vector3 move;
    [SerializeField] private RaycastHit2D hit;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Debug.Log(x);
        Debug.Log(y);

        //reset moveDelta
        move = Vector3.zero;
        move = new Vector3(x, y, 0);

        //hoán đổi hướng sprite, cho dù đi sang trái hay phải
        if(move.x>0)//right
        {
            transform.localScale = Vector3.one;
        }else if (move.x < 0)//left
        {
            transform.localScale = new Vector3(-1, 1, 1);   
        }

        //đảm bảo rằng có thể di chuyển theo hướng này, bằng cách truyền một hộp ở đó trước, nếu hộp trả về null, thi có thể tự do di chuyển
        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(0, move.y), Mathf.Abs(move.y * speed), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //làm vật di chuyển
            transform.Translate(0,move.y * speed, 0);
        }//X

        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector2(move.x,0), Mathf.Abs(move.x * speed), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //làm vật di chuyển
            transform.Translate(move.x * speed, 0, 0);
        }//Y
    }
}
